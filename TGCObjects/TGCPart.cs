using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    public class TGCPart : TGCObjectBase
    {

        #region Public Properties
        /// <summary>
        /// The unique id for this part. It will never change.
        /// </summary>
        public string id { get { return GetProperty("id") as string; } }
        /// <summary>
        /// part.
        /// </summary>
        public string object_type { get { return GetProperty("object_type") as string; } }
        /// <summary>
        /// A date when the part was created.
        /// </summary>
        public string date_created { get { return GetProperty("date_created") as string; } }
        /// <summary>
        /// A date when the part was last updated.
        /// </summary>
        public string date_updated { get { return GetProperty("date_updated") as string; } }
        /// <summary>
        /// The name the part is known by.
        /// </summary>
        public string name { get { return GetProperty("name") as string; } }
        /// <summary>
        /// An integer between 1 and 2147483647. Defaults to 1. This is how many of the part are in stock.
        /// </summary>
        public string quantity { get { return GetProperty("quantity") as string; } }
        /// <summary>
        /// Sets an image of the part. The ID is from a File. If a photo_id is specified, then an extra property called photo will be added which will briefly describe the image.
        /// </summary>
        public string photo_id { get { return GetProperty("photo_id") as string; } }
        /// <summary>
        /// A full written description of the part.
        /// </summary>
        public string description { get { return GetProperty("description") as string; } }
        /// <summary>
        /// The Game Crafter's suggested retail price.
        /// </summary>
        public string msrp { get { return GetProperty("msrp") as string; } }
        /// <summary>
        /// The same as msrp, but represents the price for each when purchased in quantities of 10 or greater.
        /// </summary>
        public string msrp_10 { get { return GetProperty("msrp_10") as string; } }
        /// <summary>
        /// The same as msrp_10, but represents the price for each when purchased in quantities of 100 or greater.
        /// </summary>
        public string msrp_100 { get { return GetProperty("msrp_100") as string; } }
        /// <summary>
        /// The same as msrp_100, but represents the price for each when purchased in quantities of 1000 or greater.
        /// </summary>
        public string msrp_1000 { get { return GetProperty("msrp_1000") as string; } }
        /// <summary>
        /// The higher of msrp or cost.
        /// </summary>
        public string price { get { return GetProperty("price") as string; } }
        /// <summary>
        /// The same as price, but represents the price for each when purchased in quantities of 10 or greater.
        /// </summary>
        public string price_10 { get { return GetProperty("price_10") as string; } }
        /// <summary>
        /// The same as price_10, but represents the price for each when purchased in quantities of 100 or greater.
        /// </summary>
        public string price_100 { get { return GetProperty("price_100") as string; } }
        /// <summary>
        /// The same as price_100, but represents the price for each when purchased in quantities of 1000 or greater.
        /// </summary>
        public string price_1000 { get { return GetProperty("price_1000") as string; } }
        /// <summary>
        /// The stock keeping unit to be used when adding this to the Cart.
        /// </summary>
        public string sku_id { get { return GetProperty("sku_id") as string; } }
        /// <summary>
        /// The weight of this part in pounds.
        /// </summary>
        public string weight { get { return GetProperty("weight") as string; } }
        /// <summary>
        /// The color of the part. Defaults to Pictured. See https://www.thegamecrafter.com/developer/Part.html for details.
        /// </summary>
        public string color { get { return GetProperty("color") as string; } }
        /// <summary>
        /// Basic taxonomy for the part. Defaults to Miscellaneous. See https://www.thegamecrafter.com/developer/Part.html for details.
        /// </summary>
        public string category { get { return GetProperty("category") as string; } }
        /// <summary>
        /// The list of GamePart that use this part.
        /// </summary>
        public List<TGCGamePart> gameparts { get { return GetPropertyList<TGCGamePart>("gameparts"); } }
        /// <summary>
        /// Returns a list of similar parts. See "Similar Parts" below.
        /// </summary>
        public List<TGCPart> similar { get { return GetPropertyList<TGCPart>("similar"); } }

        #endregion

        #region Constructors and Initialization
        public TGCPart()
            : base()
        {
            Initialize();
        }
        public TGCPart(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            Initialize();
        }
        public TGCPart(string apiPubKey)
            : base(apiPubKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            URI = BaseURI + "part";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a part
        /// </summary>
        /// <param name="session">The session used to create the part</param>
        /// <param name="designerName">The name of the part to create</param>
        /// <param name="user">The user to create the part under</param>
        /// <param name="optionalParams">See <see cref="https://www.thegamecrafter.com/developer/Designer.html"/> for details</param>
        /// 
        /// <returns>Returns the newly created designer</returns>
        public static TGCPart CreatePart(TGCSession session, string name, params TGCParameter[] optionalParams)
        {
            var requiredParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", name)
            };
            var callParams = requiredParams.ToList();
            callParams.AddRange(optionalParams);
            var request = new TGCWebRequest(BaseURI + "part", callParams.ToArray());
            var response = request.Post();

            var newPart = new TGCPart(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            newPart.rawresult = response.ResponseString;
            newPart.Parse();
            return newPart;
        }

        public void Update()
        {
            var requiredParams = new TGCParameter[]
            {
                new TGCParameter("session_id", TGCSession.Current.id),
                new TGCParameter("name", name)
            };
            var optionalParams = TGCParameter.CreateParameterList(result, 
                "photo_id", "weight", "quantity", "low_quantity", "color", "category",
                "notes", "description", "msrp", "msrp_10", "msrp_100", "msrp_1000", "cost");
            optionalParams.AddRange(requiredParams);
            var request = new TGCWebRequest(this.URI + "/" + this.id, optionalParams.ToArray());
            var response = request.Put();
        }

        /// <summary>
        /// Deletes the part on the server
        /// </summary>
        /// <param name="session">The session used to delete the part</param>
        /// <returns>Returns success as boolean</returns>
        public bool Delete(TGCSession session)
        {
            var request = new TGCWebRequest(this.URI + "/" + this.id, new TGCParameter("session_id", session.id));
            var response = request.Delete();

            var success = response.ResponseString.Contains("1");
            return success;
        }

        /// <summary>
        /// Fetches the part with the given ID from the server
        /// </summary>
        /// <param name="id">The ID of the part to get</param>
        /// <param name="session">Optional - The session to get the part with</param>
        /// <returns>Returns the part with the given ID</returns>
        public static TGCPart FetchPart(string id, TGCSession session = null)
        {
            var uri = BaseURI + "part/" + id;
            TGCWebRequest request;
            TGCPart part;
            var includeRelationships = new TGCParameter("_include_relationships", "1");
            if (session != null)
            {
                request = new TGCWebRequest(uri, new TGCParameter("session", session.id), includeRelationships);
                part = new TGCPart(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            }
            else
            {
                request = new TGCWebRequest(uri, includeRelationships);
                part = new TGCPart();
            }
            var response = request.Get();
            part.rawresult = response.ResponseString;
            part.Parse();
            return part;
        }
        #endregion
    }
}
