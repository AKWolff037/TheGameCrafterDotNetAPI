using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace TGCDotNetAPI
{
    /// <summary>
    /// The response of a TGCWebRequest
    /// </summary>
    [Serializable]
    public class TGCResultResponse : TGCObjectBase
    {

        #region Constructors and Initialization
        public TGCResultResponse()
            : base()
        {
            Initialize();
        }
        public TGCResultResponse(string apiPubKey)
            : base(apiPubKey)
        {
            Initialize();
        }
        public TGCResultResponse(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            
        }
        #endregion
    }
}
