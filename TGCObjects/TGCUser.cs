using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// This class represents a User object on the TGC site, along with all of the properties that are available
    /// </summary>
    [Serializable]
    public class TGCUser : TGCObjectBase
    {
        //Each user is accessed via /api/user

        #region Public Properties
        /// <summary> The unique id for this user. It will never change </summary>
        public string id
        {
            get { return GetProperty("id") as string; }
        }
        /// <summary>
        /// Wing object type should always return user
        /// </summary>
        public string wing_object_type
        {
            get { return GetProperty("object_type") as string; }
        }
        /// <summary>
        /// Another unique way of identifying a user. However, this can be changed by an admin or the user himself.
        /// </summary>
        public string username
        {
            get { return GetProperty("username") as string; }
        }

        /// <summary>
        /// The password for the user.  This will never be returned by an API call, but must be input in order to do a session login
        /// </summary>
        public string password
        {
            get { return GetProperty("password") as string; }
        }
        /// <summary>
        /// //The name that this user goes by in the real world. Example: Andy Dufresne
        /// </summary>
        public string real_name
        {
            get { return GetProperty("real_name") as string; }
        }
        /// <summary>
        /// An email address associated with this user
        /// </summary>
        public string email
        {
            get { return GetProperty("email") as string; }
        }
        /// <summary>
        /// Which field should be used to determine a display name. Defaults to username. Options are username, real_name, email.
        /// </summary>
        public string use_as_display_name
        {
            get { return GetProperty("use_as_display_name") as string; }
        }
        /// <summary>
        /// Only used for display purposes. See also use_as_display_name.
        /// </summary>
        public string display_name
        {
            get { return GetProperty("display_name") as string; }
        }
        /// <summary>
        /// A boolean indicating whether the user has admin rights on the server.
        /// </summary>
        public string admin
        {
            get { return GetProperty("admin") as string; }
        }
        /// <summary>
        /// The amount of shop credit owned by this user
        /// </summary>
        public string shop_credit
        {
            get { return GetProperty("shop_credit") as string; }
        }
        /// <summary>
        /// The amount of crafter points for this user.
        /// </summary>
        public string crafter_points
        {
            get { return GetProperty("crafter_points") as string; }
        }
        /// <summary>
        /// A boolean indicating whether the user has been granted the privilege of checking out using an invoice.
        /// </summary>
        public string approved_for_invoice
        {
            get { return GetProperty("approved_for_invoice") as string; }
        }
        /// <summary>
        /// No API documentation given - the skype username for the user that they've input into the site
        /// </summary>
        public string skype
        {
            get { return GetProperty("skype") as string; }
        }
        /// <summary>
        /// A date when the user last logged in to the system.
        /// </summary>
        public string last_login
        {
            get { return GetProperty("last_login") as string; }
        }
        /// <summary>
        /// A date when the user was created.
        /// </summary>
        public string date_created
        {
            get { return GetProperty("date_created") as string; }
        }
        /// <summary>
        /// A date when the user's account was last updated.
        /// </summary>
        public string date_updated
        {
            get { return GetProperty("date_updated") as string; }
        }
        /// <summary>
        /// The id of the Folder where this user can upload files. When a user is created, a separate Folder is made for them.
        /// </summary>
        public string root_folder_id
        {
            get { return GetProperty("root_folder_id") as string; }
        }
        /// <summary>
        /// The unique id of the avatar for this user.
        /// </summary>
        public string avatar_id
        {
            get { return GetProperty("avatar_id") as string; }
        }
        /// <summary>
        /// The URI of the avatar for this user.
        /// </summary>
        public string avatar_uri
        {
            get { return GetProperty("avatar_uri") as string; }
        }
        public string is_admin 
        { 
            get { return GetProperty("is_admin") as string; } 
        }
        public string shipping_victim
        {
            get { return GetProperty("shipping_victim") as string; }
        }
        public string is_crafter
        {
            get { return GetProperty("is_crafter") as string; }
        }
        public string edit_uri
        {
            get { return GetProperty("edit_uri") as string; }
        }
        public string avatar_size
        {
            get { return GetProperty("avatar_size") as string; }
        }
        public string crafter
        {
            get { return GetProperty("crafter") as string; }
        }
        public string is_approved_for_invoice
        {
            get { return GetProperty("is_approved_for_invoice") as string; }
        }
        public string facebook_uid
        {
            get { return GetProperty("facebook_uid") as string; }
        }
        public string facebook_token
        {
            get { return GetProperty("facebook_token") as string; }
        }
        public string perfectionist
        {
            get { return GetProperty("perfectionist") as string; }
        }
        public string developer
        {
            get { return GetProperty("developer") as string; }
        }
        public string vip
        {
            get { return GetProperty("vip") as string; }
        }
        public string can_edit
        {
            get { return GetProperty("can_edit") as string; }
        }
        public string is_curator
        {
            get { return GetProperty("is_curator") as string; }
        }
        public string is_developer
        {
            get { return GetProperty("is_developer") as string; }
        }
        public string copyright_violator
        {
            get { return GetProperty("copyright_violator") as string; }
        }
        /// <summary>
        /// The starting Folder where this user can upload files
        /// </summary>
        public TGCFolder root_folder
        {
            get { return GetProperty<TGCFolder>("root_folder"); }
        }
        /// <summary>
        /// The list of root Folders controlled by this user. More specifically, those directly under the root_folder.
        /// </summary>
        public List<TGCFolder> folders
        {
            get { return GetPropertyList<TGCFolder>("folders"); }
        }
        /// <summary>
        /// The list of Designers this user controls.
        /// </summary>
        public List<TGCDesigner> designers { get { return GetPropertyList<TGCDesigner>("designers"); } }
        /// <summary>
        /// The list of Carts the user has created.
        /// </summary>
        //public List<TGCCart> carts;
        /// <summary>
        /// The list of Receipts for previous orders the user has created.
        /// </summary>
        //public List<TGCReceipt> receipts;
        /// <summary>
        /// The list of Reviews the user has created.
        /// </summary>
        //public List<TGCReview> reviews;
        /// <summary>
        /// The list of Wishlists the user has created.
        /// </summary>
        //public List<TGCWishlist> wishlists;

        #endregion

        #region Constructors and Initialization
        public TGCUser()
            : base()
        {
            Initialize();
        }
        private TGCUser(string apiKey) :  base(apiKey)
        {
            Initialize();
        }
        public TGCUser(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            URI = BaseURI + "user";
        }
        public TGCUser(string apiPubKey, string userName, string pswrd) : base(apiPubKey)
        {
            URI = BaseURI + "user";
            SetProperty("username", userName);
            SetProperty("password", pswrd);
        }
        public TGCUser(string apiPubKey, string apiPrivKey, string userName, string pswrd)
            : base(apiPubKey, apiPrivKey)
        {
            URI = BaseURI + "user";
            SetProperty("username", userName);
            SetProperty("password", pswrd);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Updates the user based on the parameters that are sent in
        /// </summary>
        /// <param name="parameters">The properties of the user that need updated</param>
        public void UpdateUser(params TGCParameter[] parameters)
        {
            URI = BaseURI + "user/" + id;
            var request = new TGCWebRequest(this, parameters);
            var response = request.Post();
            this.rawresult = response.ResponseString;
            this.Parse();
        }
        /// <summary>
        /// Deletes a user
        /// </summary>
        public void DeleteUser()
        {
            URI = BaseURI + "user/" + id;
            var request = new TGCWebRequest(this);
            var response = request.Delete();
            this.rawresult = response.ResponseString;
            this.Parse();
        }
        /// <summary>
        /// Fetches a user's information
        /// </summary>
        /// <param name="userId">The user ID to fetch info for</param>
        /// <param name="sessionId">The session ID to use</param>
        public void FetchUser(string userId, string sessionId)
        {
            SetProperty("id", userId);
            FetchUser(sessionId);
        }
        /// <summary>
        /// Fetches a user based on a session, including relationships and related objects
        /// </summary>
        /// <param name="sessionId">The session to use to retrieve user info</param>
        public void FetchUser(string sessionId)
        {
            URI = BaseURI + "user/" + id;
            var sessionParam = new TGCParameter("session_id", sessionId);
            var includeRelationships = new TGCParameter("_include_relationships", "1");
            var includeRelatedObjects = new TGCParameter("_include_related_objects", "1");
            var request = new TGCWebRequest(this, sessionParam, includeRelatedObjects, includeRelationships);
            var response = request.Get();
            this.rawresult = response.ResponseString;
            this.Parse();
        }
        #endregion
    }
}
