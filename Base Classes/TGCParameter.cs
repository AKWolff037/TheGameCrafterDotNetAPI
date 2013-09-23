using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// A interface used to represent a parameter into the TGC service
    /// </summary>
    public interface ITGCParameter
    {
        /// <summary>
        /// The key that the parameter should be called with
        /// </summary>
        string Key { get; }
        /// <summary>
        /// A string value, if any, that the parameter represents
        /// </summary>
        string Value { get; }
        /// <summary>
        /// A string value 
        /// </summary>
        byte[] Data { get; }
        System.IO.Stream DataStream { get; }
    }
    public class TGCParameter : ITGCParameter
    {
        #region Public Properties
        /// <summary>
        /// The key of the parameter
        /// </summary>
        public string Key { get; internal set; }
        /// <summary>
        /// The string value, if applicable, of the parameter
        /// </summary>
        public string Value { get; internal set; }
        /// <summary>
        /// The byte[] value of the data, if applicable, of the paramter
        /// </summary>
        public byte[] Data { get; internal set; }
        /// <summary>
        /// A stream object containing the Data information
        /// </summary>
        public System.IO.Stream DataStream { get; internal set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new parameter with a string value
        /// </summary>
        /// <param name="key">The name of the parameter</param>
        /// <param name="value">The string value of the parameter</param>
        public TGCParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public TGCParameter(string key, byte[] value)
        {
            Key = key;
            Data = value;
            DataStream = new System.IO.MemoryStream(value);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a parameter list from the source dictionary based on the keys passed in
        /// </summary>
        /// <param name="source">The source dictionary to create the parameters from</param>
        /// <param name="keys">The keys to include in the parameter list</param>
        /// <returns>Returns a list of parameters based on their key values</returns>
        public static List<TGCParameter> CreateParameterList(Dictionary<string, object> source, params string[] keys)
        {
            var list = new List<TGCParameter>();
            foreach (string key in keys)
            {
                var value = TGCObjectBase.FindKeyInDictionary(source, key);
                if (value == null)
                {
                    continue;
                }
                else if (value is byte[])
                {
                    var param = new TGCParameter(key, value as byte[]);
                    list.Add(param);
                }
                else if (value is string)
                {
                    var param = new TGCParameter(key, value as string);
                    list.Add(param);
                }
                else if (value is ITGCObject)
                {
                    var param = new TGCParameter(key, (value as ITGCObject).GetProperty("id") as string);
                    list.Add(param);
                }
            }
            return list;
        }
        #endregion
    }
}
