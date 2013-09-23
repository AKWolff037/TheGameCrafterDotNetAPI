using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    [Serializable]
    public class TGCDesigner : TGCObjectBase
    {
        #region Public Properties
        /// <summary>
        /// The unique id for this designer. It will never change
        /// </summary>
        public string id { get { return GetProperty("id") as string; } }
        /// <summary>
        /// designer.
        /// </summary>
        public string object_type { get { return GetProperty("object_type") as string; } }
        /// <summary>
        /// Another unique way of identifying a designer. However, this can be changed by an admin or the user that controls the designer.
        /// </summary>
        public string name { get { return GetProperty("name") as string; } }
        /// <summary>
        /// The unique id of a user that has control over a designer, and therefore the games that the designer owns.
        /// </summary>
        public string user_id { get { return GetProperty("user_id") as string; } }
        /// <summary>
        /// An email address associated with this designer. This will be publicly available so people can contact the designer.
        /// </summary>
        public string contact_email { get { return GetProperty("contact_email") as string; } }
        /// <summary>
        /// A URI for the designer's public web site.
        /// </summary>
        public string website_uri { get { return GetProperty("website_uri") as string; } }
        /// <summary>
        /// A URI for the designer's profile in the shop.
        /// </summary>
        public string shop_uri { get { return GetProperty("shop_uri") as string; } }
        /// <summary>
        /// A date when the designer was created.
        /// </summary>
        public string date_created { get { return GetProperty("date_created") as string; } }
        /// <summary>
        /// A date when the designer was last updated.
        /// </summary>
        public string date_updated { get { return GetProperty("date_updated") as string; } }
        /// <summary>
        /// Sets the logo. The ID is from a File. If a logo_id is specified, then an extra property called logo will be added which will briefly describe the image.
        /// </summary>
        public string logo_id { get { return GetProperty("logo_id") as string; } }
        /// <summary>
        /// Determines how a designer's profits will be distributed. Defaults to shop_credit. Options are: shop_credit and paypal.
        /// </summary>
        public string payout_via { get { return GetProperty("payout_via") as string; } }
        /// <summary>
        /// The email address of the designer's PayPal account. Only used if payout_via is set to paypal.
        /// </summary>
        public string paypal_email { get { return GetProperty("paypal_email") as string; } }
        /// <summary>
        /// The list of Games that are controlled by this designer.
        /// </summary>
        public List<TGCGame> games { get { return GetPropertyList<TGCGame>("games"); } }
        /// <summary>
        /// The User that controls this designer.
        /// </summary>
        public TGCUser user { get { return GetProperty<TGCUser>("user"); } }

        #endregion

        #region Constructors and Initialization
        public TGCDesigner()
            : base()
        {
            Initialize();
        }
        public TGCDesigner(string pubAPIKey)
            : base(pubAPIKey)
        {
            Initialize();
        }
        public TGCDesigner(string pubKey, string privKey)
            : base(pubKey, privKey)
        {
            Initialize();
        }

        public void Initialize()
        {
            URI = BaseURI + "/designer";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a designer
        /// </summary>
        /// <param name="session">The session used to create the designer</param>
        /// <param name="designerName">The name of the designer to create</param>
        /// <param name="user">The user to create the designer under</param>
        /// <param name="optionalParams">Optional params include: contact_email, website_uri, logo_id, payout_via, paypal_email       
        /// <see cref="https://www.thegamecrafter.com/developer/Designer.html"/>
        /// </param>
        /// 
        /// <returns>Returns the newly created designer</returns>
        public static TGCDesigner CreateDesigner(TGCSession session, string designerName, TGCUser user, params TGCParameter[] optionalParams)
        {
            var requiredParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", designerName),
                new TGCParameter("user_id", user.id)
            };
            var callParams = requiredParams.ToList();
            callParams.AddRange(optionalParams);
            var request = new TGCWebRequest(BaseURI + "designer", callParams.ToArray());
            var response = request.Post();

            var newDesigner = new TGCDesigner(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            newDesigner.rawresult = response.ResponseString;
            newDesigner.Parse();
            return newDesigner;
        }

        public void Update()
        {
            var requiredParams = new TGCParameter[]
            {
                new TGCParameter("session_id", TGCSession.Current.id),
                new TGCParameter("name", name),
                new TGCParameter("user_id", this.user_id)
            };
            var optionalParams = TGCParameter.CreateParameterList(result, "contact_email", "website_uri", "logo_id", "payout_via", "paypal_email");
            optionalParams.AddRange(requiredParams);
            var request = new TGCWebRequest(this.URI + "/" + this.id, optionalParams.ToArray());
            var response = request.Put();            
        }
        /// <summary>
        /// Deletes the designer on the server
        /// </summary>
        /// <param name="session">The session used to delete the designer</param>
        /// <returns>Returns success as boolean</returns>
        public bool Delete(TGCSession session)
        {
            var request = new TGCWebRequest(this.URI + "/" + this.id, new TGCParameter("session_id", session.id));
            var response = request.Delete();

            var success = response.ResponseString.Contains("1");
            return success;
        }

        /// <summary>
        /// Fetches the designer with the given ID from the server
        /// </summary>
        /// <param name="id">The ID of the designer to get</param>
        /// <param name="session">Optional - The session to get the designer with</param>
        /// <returns>Returns the designer with the given ID</returns>
        public static TGCDesigner FetchDesigner(string id, TGCSession session = null)
        {
            var uri = BaseURI + "designer/" + id;
            TGCWebRequest request;
            TGCDesigner designer;
            var includeRelationships = new TGCParameter("_include_relationships", "1");
            if (session != null)
            {
                request = new TGCWebRequest(uri, new TGCParameter("session", session.id), includeRelationships);
                designer = new TGCDesigner(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            }
            else
            {
                request = new TGCWebRequest(uri, includeRelationships);
                designer = new TGCDesigner();
            }
            var response = request.Get();
            designer.rawresult = response.ResponseString;
            designer.Parse();
            return designer;
        }

        #endregion
    }
}
