using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// A Web Request containing the necessary information to interact with the TGC Wing API
    /// </summary>
    public interface ITGCWebRequest
    {
        /// <summary>
        /// A callback method for asynchronous calls
        /// </summary>
        AsyncCallback WebResponseCallback { get; }
        /// <summary>
        /// A list of parameters to add into the web request
        /// </summary>
        List<ITGCParameter> Parameters { get; }
        /// <summary>
        /// The URI to make the web request against
        /// </summary>
        string URI { get; }
        /// <summary>
        /// The response returned after the request is made
        /// </summary>
        ITGCWebResponse Response { get; }
    }
}
