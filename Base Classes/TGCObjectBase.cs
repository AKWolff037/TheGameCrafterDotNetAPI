using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace TGCDotNetAPI
{
    /// <summary>
    /// This abstract class stands as the base for any object that we want to get from a TGCWebResponse.
    /// All of the objects in the TGC API should inherit from this class.
    /// </summary>
    /// <remarks>
    /// It uses a JavaScriptSerializer object to deserialize the JSON repsonse into a string/object dictionary
    /// The parse methods should work on all types that inherit the base, so classes that inherit should be able to use
    /// all of the base methods without a problem
    /// </remarks>
    /// <author>Alex Wolff</author>
    [Serializable]
    public abstract class TGCObjectBase : ITGCObject
    {
        #region Properties
        /// <summary>
        /// The parsed result of the web response
        /// </summary>
        public Dictionary<string, object> result { get; set; }
        /// <summary>
        /// The unparsed raw JSON string of the web response
        /// </summary>
        public string rawresult { get; set; } 

        //You need both a public and private API key to use the service
        private string _apiPubKey = string.Empty;
        private string _apiPrivKey = string.Empty;
        /// <summary>
        /// The Public API Key that the developer is using
        /// </summary>
        public string API_PUBLIC_KEY { get { return _apiPubKey; } set { _apiPubKey = value; } }
        /// <summary>
        /// The Private API Key that the developer is using, used for SSO
        /// </summary>
        public string API_PRIVATE_KEY { get { return _apiPrivKey; } set { _apiPrivKey = value; } }
        /// <summary>
        /// This is the base URI for all calls to the TGC API
        /// </summary>
        public const string BaseURI = "https://www.thegamecrafter.com/api/";
        /// <summary>
        /// The URI should be overridden for each inheriting object with that particular object's API.  In most cases it will be the BaseURI + the object name
        /// </summary>
        public virtual string URI { get; internal set; }
        #endregion

        #region Constructors and Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        public TGCObjectBase()
        {
            _apiPubKey = string.Empty;
            _apiPrivKey = string.Empty;
            Initialize();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiPublicKey">The Public API Key</param>
        public TGCObjectBase(string apiPublicKey)
        {
            Initialize();
            _apiPubKey = apiPublicKey;
            _apiPrivKey = string.Empty;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiPublicKey">The Public API Key</param>
        /// <param name="apiPrivateKey">The Private API Key</param>
        public TGCObjectBase(string apiPublicKey, string apiPrivateKey)
        {
            Initialize();
            _apiPubKey = apiPublicKey;
            _apiPrivKey = apiPrivateKey;
        }
        /// <summary>
        /// Private method to initialize the object - called by the constructors
        /// </summary>
        private void Initialize()
        {
            result = new Dictionary<string, object>();
        }

        #endregion

        #region API Key Methods
        /// <summary>
        /// Sets the public API Key
        /// </summary>
        /// <param name="apiKey">The value to set the Public API Key to</param>
        public void SetPublicAPIKey(string apiKey)
        {
            _apiPubKey = apiKey;
        }
        /// <summary>
        /// Sets the private API Key
        /// </summary>
        /// <param name="apiKey">The value to set the Private API Key to</param>
        public void SetPrivateAPIKey(string apiKey)
        {
            _apiPrivKey = apiKey;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Parse the value in rawresult and store it in result using the JavaScriptSerializer
        /// </summary>
        public virtual void Parse()
        {
            var ser = new JavaScriptSerializer();
            var deserialized = ser.Deserialize<Dictionary<string, object>>(rawresult);
            this.result = deserialized;
        }

       /// <summary>
       /// Set/Update a value in the result dictionary with the given key and value
       /// If the dictionary does not contain the key, it will add it
       /// </summary>
       /// <param name="key">The key or property name to set</param>
       /// <param name="value">The value to set it to</param>
        public void SetProperty(string key, object value)
        {
            if (result == null)
            {
                return;
            }
            else if (result.ContainsKey(key))
            {
                result[key] = value;
            }
            else
            {
                result.Add(key, value);
            }
        }
        
        /// <summary>
        /// Gets a list of the given TGC Object
        /// </summary>
        /// <typeparam name="T">The type of TGC Object to return a list of</typeparam>
        /// <param name="key">The key in the dictionary to retrieve the list from</param>
        /// <returns>Returns a List of type T</returns>
        public List<T> GetPropertyList<T>(string key) where T : TGCObjectBase, new()
        {
            //Initialize a new list so that we avoid returning null
            var list = new List<T>();
            var serializer = new JavaScriptSerializer();
            var callURI = FindKeyInDictionary(result, key) as string;
            //If the result dictionary contains the property key, do something...
            if (callURI != null)
            {
                var requestURI = "https://www.thegamecrafter.com" + callURI;
                var request = new TGCWebRequest(requestURI, new TGCParameter("session_id", TGCSession.Current.id));
                var response = request.Get();
                //Convert the response to the desired type of object
                var jsserializer = new JavaScriptSerializer();
                var responseDict = jsserializer.Deserialize<Dictionary<string, object>>(response.ResponseString);
                var arylist = FindKeyInDictionary(responseDict, "items") as System.Collections.ArrayList;
                foreach (object obj in arylist)
                {
                    var typedObj = new T();
                    typedObj.result = obj as Dictionary<string, object>;                    
                    list.Add(typedObj);
                }
            }
            return list;
        }
        /// <summary>
        /// Gets a property value as a TGCObject for a given dictionary key
        /// </summary>
        /// <typeparam name="T">The type of TGCObject to return, must inherit TGCObjectBase</typeparam>
        /// <param name="key">The key in the dictionary to search on</param>
        /// <returns>Returns the found key as the given type or null if no key is found</returns>
        public T GetProperty<T>(string key) where T : TGCObjectBase, new()
        {
            //Create a new object of the given type
            var tcgobj = new T();
            //Set the API keys based on the parent object
            tcgobj._apiPubKey = this._apiPubKey;
            tcgobj._apiPrivKey = this._apiPrivKey;
            //Check through all possible levels of the result dictionary to find the given key
            var value = FindKeyInDictionary(result, key);
            if (value != null)
            {
                return ParseResult<T>(value, tcgobj);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// This will return a property value for a key in the given dictionary, searching through all sub-levels if appropriate
        /// </summary>
        /// <param name="key">The key to find in the dictionary</param>
        /// <returns>Returns the property if found or null if no key is found</returns>
        public object GetProperty(string key)
        {
            var property = FindKeyInDictionary(result, key);
            return property == null 
                        ? string.Empty 
                        : property;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// This will search through all levels of the dictionary through recursion looking for the key
        /// </summary>
        /// <param name="dict">The dictionary to search through</param>
        /// <param name="key">The key to find</param>
        /// <returns>The value found at the given key</returns>
        public static object FindKeyInDictionary(Dictionary<string, object> dict, string key)
        {
            if (dict == null)
                return null;
            //Search the first level of the dictionary
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            else
            {
                //Search each sub-level of the dictionary and recursively call this function to check the further levels
                //Use the LINQ where to make sure the type is correct
                foreach (Dictionary<string, object> subDict in dict.Values.Where(val => val is Dictionary<string, object>))
                {
                    var result = FindKeyInDictionary(subDict, key);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Parse the object in the dictionary as a TGCObject
        /// This will accept input as a TGC object, JSON string, or a string/object dictionary
        /// </summary>
        /// <typeparam name="T">The type of TGC Object to parse</typeparam>
        /// <param name="input">The input to parse off of</param>
        /// <param name="obj">The TGC Object reference to parse into if appropriate</param>
        /// <returns>Returns the parsed object or null if there was bad input</returns>
        public static T ParseResult<T>(object input, T obj) where T : TGCObjectBase
        {
            if (input is T)
            {
                return input as T;
            }
            else if (input is Dictionary<string, object>)
            {
                obj.result = input as Dictionary<string, object>;
                return obj;
            }
            else if (input is string)
            {
                obj.rawresult = input as string;
                obj.Parse();
                return obj;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region IEnumerable<ITGCObject> Members
        /// <summary>
        /// Returns an inherited class as an IEnumerable
        /// </summary>
        /// <typeparam name="T">The type to get an Enumerator for, must inherit ITGCObject</typeparam>
        /// <returns>Returns the enumerator for T</returns>
        public IEnumerator<T> GetEnumerator<T>() where T : ITGCObject
        {
            return GetEnumerator() as IEnumerator<T>;
        }
        /// <summary>
        /// Found on Stack Overflow as a way to easily implement IEnumerator
        /// </summary>
        /// <returns>Returns this as IEnumerator<ITGCObject></returns>
        public IEnumerator<ITGCObject> GetEnumerator()
        {
            yield return this;
        }

        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Returns this an as enumerator
        /// </summary>
        /// <returns>Returns this as an enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            yield return this;
        }

        #endregion
    }
}
