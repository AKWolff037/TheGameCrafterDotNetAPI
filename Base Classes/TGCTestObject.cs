using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TGCDotNetAPI
{
    [Serializable]
    public class TGCTestObject : TGCObjectBase
    {
        public TGCTestObject(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            URI = BaseURI + "_test";
        }
        public TGCTestObject(string apiKey) : base(apiKey)
        {
            URI = BaseURI + "_test";
        }

        public ITGCWebResponse Get()
        {
            var request = new TGCWebRequest(this, new TGCParameter("_testGetParameter", "TestGetValue"), new TGCParameter("foo", "bar"));
            return request.Get();
        }

        public ITGCWebResponse Post()
        {
            var request = new TGCWebRequest(this, new TGCParameter("_testPostParameter", "TestPostValue"), new TGCParameter("foo", "bar"));
            return request.Post();
        }

        public ITGCWebResponse Put()
        {
            var request = new TGCWebRequest(this, new TGCParameter("_testPutParameter", "TestPutValue"), new TGCParameter("foo", "bar"));
            return request.Put();
        }

        public ITGCWebResponse Delete()
        {
            var request = new TGCWebRequest(this, new TGCParameter("_testDeleteParameter", "TestDeleteValue"), new TGCParameter("foo", "bar"));
            return request.Delete();
        }
    }
}
