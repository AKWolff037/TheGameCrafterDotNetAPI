using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace TGCDotNetAPI
{
    /// <summary>
    /// A warning response from a TGCWebRequest
    /// </summary>
    [Serializable]
    public class TGCWarningResponse : TGCObjectBase
    {
        /// <summary>
        /// The code is always an integer and conforms to the standard list of Wing ErrorCodes.
        /// </summary>
        public int code { get; private set; }
        /// <summary>
        /// The message is a human readable message that you can display to a user.
        /// </summary>
        public string message { get; private set; }
        /// <summary>
        /// The data element many times will be null, but it can have important debug information. For example, if a required field was left empty, the field name could be put in the data element so that the app could highlight the field for the user.
        /// </summary>
        public object data { get; private set; }

        #region Constructors and Initialization
        public TGCWarningResponse()
            : base()
        {
            Initialize();
        }
        public TGCWarningResponse(string apiKey) : base(apiKey)
        {
            Initialize();
        }
        public TGCWarningResponse(string pubApiKey, string privApiKey)
            : base(pubApiKey, privApiKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            URI = BaseURI + "warning";
        }
        #endregion
    }
}
