using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
namespace TGCDotNetAPI
{
    /// <summary>
    /// The response object from a TGCWebResponse
    /// </summary>
    [Serializable]
    public class TGCWebResponse : ITGCWebResponse
    {
        #region Private Variables
        private string _responseString;
        #endregion

        #region Public Properties
        /// <summary>
        /// The exception that returned from the request, null if successful request
        /// </summary>
        public WebException Exception { get; private set; }
        /// <summary>
        /// The error message returned from the web request
        /// </summary>
        public ITGCObject Error { get; private set; }
        /// <summary>
        /// The result message returned from the web request
        /// </summary>
        public ITGCObject Result { get; private set; }
        /// <summary>
        /// The response JSON string
        /// </summary>
        public string ResponseString
        {
            get
            {
                return _responseString;
            }
            set
            {
                if (_responseString != value)
                {
                    _responseString = value;
                }
            }
        }
        #endregion

        #region Constructors
        internal TGCWebResponse(WebException wex)
            : base()
        {
            ResponseString = string.Empty;
            Exception = wex;
        }
        public TGCWebResponse(WebResponse baseResponse) : base()
        {
            var responseStrm = baseResponse.GetResponseStream();
            var strmReader = new System.IO.StreamReader(responseStrm);
            var responseString = strmReader.ReadToEnd();
            ResponseString = responseString;
            //parse errors and result
            if (responseString.IndexOf("error:") == -1)
            {
                var resultResponse = new TGCResultResponse();
                resultResponse.rawresult = responseString;
                resultResponse.Parse();
                Result = resultResponse;
            }
            else
            {
                var errorResponse = new TGCErrorResponse();
                errorResponse.rawresult = responseString;
                errorResponse.Parse();
                Error = errorResponse;
            }
            Exception = null;
        }
        #endregion

        #region Property Overrides
        /// <summary>
        /// The Repsonse String
        /// </summary>
        /// <returns>The Response String</returns>
        public override string ToString()
        {
            return ResponseString;
        }
        #endregion
    }
}
