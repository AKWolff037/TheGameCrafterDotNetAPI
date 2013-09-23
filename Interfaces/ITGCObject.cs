using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// The TGCObject and all classes that inherit it should also inherit these properties as well
    /// </summary>
    public interface ITGCObject : ISerializable, IEnumerable<ITGCObject>
    {
        /// <summary>
        /// The Public API key of the developer
        /// </summary>
        string API_PUBLIC_KEY { get; set; }
        /// <summary>
        /// The Private API key of the developer (used for SSO)
        /// </summary>
        string API_PRIVATE_KEY { get; set; }
        /// <summary>
        /// The URI that should be called by the web request
        /// </summary>
        string URI { get; }
    }
}
