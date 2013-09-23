using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace TGCDotNetAPI
{
    /// <summary>
    /// The class through which all web requests to the TGC site should be made
    /// </summary>
    [Serializable]
    public class TGCWebRequest :  ITGCWebRequest
    {
        #region Enums
        public enum HttpAction
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        #endregion
        #region Public Properties
        /// <summary>
        /// The response returned from making the specified request
        /// </summary>
        public ITGCWebResponse Response { get; private set; }
        /// <summary>
        /// A response callback used for asynchronous calls
        /// </summary>
        public AsyncCallback WebResponseCallback {get; private set;}       
        /// <summary>
        /// The URI to make the request against
        /// </summary>
        public string URI { get; private set; }
        /// <summary>
        /// A list of parameters to add into the request URI
        /// </summary>
        public List<ITGCParameter> Parameters { get; private set; }
        #endregion

        #region Constructors and Initialization
        public TGCWebRequest(ITGCObject obj, params ITGCParameter[] parms)
            : base()
        {
            Initialize(obj.URI, parms);
        }
        public TGCWebRequest(string uri, params ITGCParameter[] parms)
            : base()
        {
            Initialize(uri, parms);
        }
        private void Initialize(string uri, params ITGCParameter[] parms)
        {
            URI = uri;
            WebResponseCallback = new AsyncCallback(OnResponseReceived);
            Parameters = new List<ITGCParameter>();
            foreach (ITGCParameter parm in parms)
            {
                Parameters.Add(parm);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Make a DELETE request and return the response
        /// </summary>
        /// <returns>Returns the TCGWebResponse</returns>
        public ITGCWebResponse Delete()
        {
            var request = MakeRequest(HttpAction.DELETE);
            return ProcessRequest(request);
        }
        /// <summary>
        /// Make a DELETE request asynchronously using the WebResponseCallback
        /// </summary>
        public void DeleteAsync()
        {
            var request = MakeRequest(HttpAction.DELETE);
            request.BeginGetResponse(WebResponseCallback, request);
        }
        /// <summary>
        /// Make a PUT request and return the response
        /// </summary>
        /// <param name="byteStream">The Byte Stream to associate with the PUT request</param>
        /// <returns>Returns the TGCWebResponse</returns>
        public ITGCWebResponse Put()
        {
            var request = MakeRequest(HttpAction.PUT);
            return ProcessRequest(request);
        }
        /// <summary>
        /// Make a PUT request asynchronously using the WebResponseCallback
        /// </summary>
        /// <param name="byteStream">The Byte Stream to associate with the PUT request</param>
        public void PutAsync()
        {
            var request = MakeRequest(HttpAction.PUT);
            request.BeginGetResponse(WebResponseCallback, request);            
        }
        /// <summary>
        /// Make a GET request and return the response
        /// </summary>
        /// <returns>Returns the TGCWebResponse</returns>
        public ITGCWebResponse Get()
        {
            var request = MakeRequest(HttpAction.GET);
            return ProcessRequest(request);
        }
        /// <summary>
        /// Make a GET request asynchronously using the WebResponseCallback
        /// </summary>
        public void GetAsync()
        {
            var request = MakeRequest(HttpAction.GET);
            request.BeginGetResponse(WebResponseCallback, request); 
        }
        /// <summary>
        /// Make a POST request and return the response
        /// </summary>
        /// <returns></returns>
        public ITGCWebResponse Post()
        {
            var request = MakeRequest(HttpAction.POST);
            return ProcessRequest(request);
        }
        /// <summary>
        /// Make a POST request and return the response
        /// </summary>
        public void PostAsync()
        {
            var request = MakeRequest(HttpAction.POST);
            request.BeginGetResponse(WebResponseCallback, request);
        }
        /// <summary>
        /// The method called by the WebResponseCallback when it receives a response
        /// </summary>
        /// <param name="result">The result of the Async call</param>
        public void OnResponseReceived(IAsyncResult result)
        {
            var webRequest = (HttpWebRequest)result.AsyncState;
            Response = ProcessRequest(webRequest);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Processes a web request and transforms it into a TGCWebResponse
        /// </summary>
        /// <param name="request">The web request to process</param>
        /// <returns>The TGC Web response</returns>
        private ITGCWebResponse ProcessRequest(WebRequest request)
        {
            try
            {
                var response = request.GetResponse();
                var tgcResponse = new TGCWebResponse(response);
                return tgcResponse;
            }
            catch (WebException wex)
            {
                Console.WriteLine("*** Exception ***");
                Console.WriteLine("Response: " + wex.Response);
                Console.WriteLine("Inner Exception:" + wex.InnerException);
                Console.WriteLine("Status:" + wex.Status);
                Console.WriteLine("Message: " + wex.Message);
                Console.WriteLine("Stack Trace: " + wex.StackTrace);
                foreach (var key in wex.Data.Keys)
                {
                    Console.WriteLine("Data [" + key.ToString() + "]: " + wex.Data[key].ToString());
                }
                return new TGCWebResponse(wex);
            }
        }
        /// <summary>
        /// Make the HTTPWebRequest with the given action and byte stream (if applicable)
        /// </summary>
        /// <param name="action">The HTTP Action to use</param>
        /// <param name="byteStream">The Byte Stream to use for a PUT request</param>
        /// <returns>Returns the HttpWebRequest with the correct action and parameter list</returns>
        private WebRequest MakeRequest(HttpAction action)
        {
            var parameterString = "?";
            WebRequest request;
            //Add the parameters that are contained in the Parameters list
            //For a GET request, add them as part of the URL
            if (action == HttpAction.GET && Parameters.Count > 0)
            {
                foreach (TGCParameter param in Parameters)
                {
                    parameterString += param.Key + "=" + param.Value + "&";
                }
                //If there are parameters entered, remove the last & from the 
                if (parameterString != string.Empty && parameterString != "?")
                {
                    parameterString = parameterString.Remove(parameterString.LastIndexOf("&"));
                }
                //Create the request with the URI given and set the action of the request
                //If the parameter string is not empty, create the request using the parameter list
                request = HttpWebRequest.Create(URI + parameterString);
                request.ContentType = "application/json";
            }
            else //If the parameter list is empty, create the request based only on the URI
            {
                request = HttpWebRequest.Create(URI);
            }
            request.Method = System.Enum.GetName(typeof(HttpAction), action);   
            //Set the content type of the Web Request
            //If there is a bytestream to use in the request, set the content length and add it
            if (Parameters.Count > 0 && action != HttpAction.GET)
            {
                string boundary = "-T3HG4M3CR4FT3R-";
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                AddParametersToRequestBody(request, Parameters, boundary);
            }

            //switch (action)
            //{
            //    case HttpAction.GET:
            //        request.ContentType = "text/html";
            //        break;
            //    case HttpAction.PUT:
            //        request.ContentType = "multipart/form-data";
            //        request.ContentLength = byteStream.Length;
            //        var strm = request.GetRequestStream();
            //        strm.Write(byteStream, 0, byteStream.Length);
            //        strm.Close();
            //        break;
            //    case HttpAction.POST:
            //        request.ContentType = "application/json";                    
            //        break;
            //    case HttpAction.DELETE:
            //        request.ContentType = "text/html";
            //        break;
            //    default:
            //        request.ContentType = "text/html";
            //        break;
            //}
            return request;
        }

        private void AddParametersToRequestBody(WebRequest request, List<ITGCParameter> parameters, string boundary)
        {
            //Create the web request body with parameters
            try
            {                
                byte[] _footer = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");

                //request.ContentLength = contentLength + _footer.Length;

                byte[] buffer = new byte[8192];
                byte[] lineEnd = Encoding.UTF8.GetBytes("\r\n");
                int read;
                var strm = request.GetRequestStream();
                {
                    foreach (ITGCParameter parm in parameters)
                    {
                        var sb = new StringBuilder();
                        var isFile = parm.Data != null;
                        var isString = parm.Value != null;
                        sb.AppendLine("--" + boundary);
                        if (isFile)
                        {
                            sb.AppendLine("Content-Disposition: form-data; name=\"file\"; filename=\"" + parm.Key + "\"");
                            sb.AppendLine("Content-Type: application/octet-stream");
                            sb.AppendLine();
                        }
                        else if (isString)
                        {
                            sb.AppendLine("Content-Disposition: form-data; name=\"" + parm.Key + "\"");
                            sb.AppendLine();
                        }
                        var headerBytes = Encoding.UTF8.GetBytes(sb.ToString());
                        strm.Write(headerBytes, 0, headerBytes.Length);
                        if (parm.Value != null)
                        {
                            var stringStrm = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(parm.Value));
                            while ((read = stringStrm.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                strm.Write(buffer, 0, read);
                            }
                            stringStrm.Dispose();
                        }
                        if (parm.Data != null)
                        {
                            while ((read = parm.DataStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                strm.Write(buffer, 0, read);
                            }
                            parm.DataStream.Dispose();
                        }
                        strm.Write(lineEnd, 0, lineEnd.Length);
                    }
                    strm.Write(_footer, 0, _footer.Length);
                }
                strm.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
