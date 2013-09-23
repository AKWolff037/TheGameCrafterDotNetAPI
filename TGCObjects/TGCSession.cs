using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// Each session is accessed via /api/session
    /// </summary>
    [Serializable]
    public class TGCSession : TGCObjectBase
    {
        private static TGCSession _current = null;
        public static TGCSession Current
        {
            get
            {
                return _current;
            }
        }

        #region Public Properties
        /// <summary>
        /// The unique id of the session. It will never change.
        /// </summary>
        public string id 
        { 
            get 
            { 
                return GetProperty("id").ToString(); 
            } 
        }
        /// <summary>
        /// The unique id of the user attached to this session.
        /// </summary>
        public string user_id 
        { 
            get 
            { 
                return GetProperty("user_id").ToString(); 
            } 
        }
        /// <summary>
        /// An integer representing the number of times this session has been extended by interacting with the server.
        /// </summary>
        public string extended 
        { 
            get 
            {
                return GetProperty("extended") as string;
            } 
        }
        /// <summary>
        /// The IP address from which this session was created.
        /// </summary>
        public string ip_address 
        { 
            get 
            { 
                return GetProperty("ip_address").ToString(); 
            } 
        }
        /// <summary>
        /// session
        /// </summary>
        public string object_type { get { return GetProperty("object_type") as string; } }
        /// <summary>
        /// The User attached to this session
        /// </summary>
        public TGCUser user 
        { 
            get 
            { 
                return GetProperty<TGCUser>("user"); 
            }   
        }
        #endregion 

        #region Constructors and Initialization
        private TGCSession()
            : base()
        {
            Initialize();
        }
        private TGCSession(string apiKey) : base(apiKey)
        {
            Initialize();
        }
        private TGCSession(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            Initialize();
        }

        private void Initialize()
        {
            URI = BaseURI + "session";
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Uses a GET to retrieve the details for the session
        /// </summary>
        public void GetDetails()
        {
            URI = BaseURI + "session/" + id;
            var request = new TGCWebRequest(this);
            var response = request.Get();
            this.rawresult = response.ResponseString;
            this.Parse();
        }
        /// <summary>
        /// Uses a POST to log in - The session's user object must be filled in
        /// Returns true if successful, false if not
        /// </summary>
        public bool Login(TGCUser loginUser = null)
        {
            try
            {
                if (loginUser == null)
                {
                    loginUser = user;
                }
                var username = new TGCParameter("username", loginUser.username);
                var pass = new TGCParameter("password", loginUser.password);
                var apikey = new TGCParameter("api_key_id", this.API_PUBLIC_KEY);
                var request = new TGCWebRequest(this, username, pass, apikey);
                var response = request.Post();
                this.rawresult = response.ResponseString;
                this.Parse();
            }
            catch
            {
                //We should do something with this exception, but i'm not sure what yet
                return false;
            }
            return true;
        }
        /// <summary>
        /// Uses a DELETE to get rid of the session
        /// Returns true if successful, false if not
        /// </summary>
        public bool Logout()
        {
            URI = BaseURI + "session/" + id;
            var request = new TGCWebRequest(this);
            var response = request.Delete();
            var success = response.ResponseString.Contains("1");
            return success;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a new session based on the user and returns it to the caller
        /// </summary>
        /// <param name="user">The user to create the session for</param>
        /// <returns>Returns the logged in session</returns>
        public static TGCSession GetNewSessionAndLogin(TGCUser user)
        {
            var session = new TGCSession(user.API_PUBLIC_KEY, user.API_PRIVATE_KEY);
            session.SetProperty("user", user);
            session.Login();
            _current = session;
            return session;
        }
        #endregion
    }
}
