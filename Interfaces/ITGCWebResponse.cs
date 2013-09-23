using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    public interface ITGCWebResponse
    {
        /// <summary>
        /// The response result as a JSON string
        /// </summary>
        string ResponseString { get; }
        /// <summary>
        /// The response result as an ITGCObject
        /// </summary>
        ITGCObject Result { get; }
        /// <summary>
        /// The response error (if exists) as an ITGCObject
        /// </summary>
        ITGCObject Error { get; }
    }
}
