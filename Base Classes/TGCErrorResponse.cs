using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace TGCDotNetAPI
{
    /// <summary>
    /// An error response coming back as a result from a TGC call
    /// </summary>
    [Serializable]
    public class TGCErrorResponse : TGCObjectBase
    {
        #region Constructors and Initialization
        public TGCErrorResponse()
            : base()
        {
            Initialize();
        }
        public TGCErrorResponse(string apiKey) : base(apiKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            URI = BaseURI + "error";
        }
        #endregion
    }
}
