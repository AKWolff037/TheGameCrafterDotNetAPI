using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace TGCDotNetAPI
{
    [Serializable]
    public class TGCFolder : TGCObjectBase
    {
        #region Public Properties
        /// <summary>
        /// The unique id for this folder. It will never change.
        /// </summary>
        public string id { get { return GetProperty("id") as string; } }
        /// <summary>
        /// folder
        /// </summary>
        public string wing_object_type { get { return GetProperty("wing_object_type") as string; } }
        /// <summary>
        /// A date when the folder was created
        /// </summary>
        public string date_created { get { return GetProperty("date_created") as string; } }
        /// <summary>
        /// A date when the folder was last updated
        /// </summary>
        public string date_updated { get { return GetProperty("date_updated") as string; } }
        /// <summary>
        /// The name the folder is known by
        /// </summary>
        public string name { get { return GetProperty("name") as string; } }
        /// <summary>
        /// The unique id of a folder that this folder is a child of
        /// </summary>
        public string parent_id { get { return GetProperty("parent_id") as string; } }
        /// <summary>
        /// The unique id of a User that controls this folder.
        /// </summary>
        public string user_id { get { return GetProperty("user_id") as string; } }
        /// <summary>
        /// The User that this folder is controlled by.
        /// </summary>
        public TGCUser user { get { return GetProperty<TGCUser>("user"); } }
        /// <summary>
        /// The Folder that this folder is contained by, if any.
        /// </summary>
        public TGCFolder parent { get { return GetProperty<TGCFolder>("parent"); } }
        /// <summary>
        /// The Folders contained in this folder.
        /// </summary>
        public List<TGCFolder> folders { get { return GetPropertyList<TGCFolder>("folders"); } }
        /// <summary>
        /// The Files contained in this folder.
        /// </summary>
        public List<TGCFile> files { get { return GetPropertyList<TGCFile>("files"); } }
        /// <summary>
        /// The Folders before this one in the hierarchy.
        /// </summary>
        public List<TGCFolder> ancestors { get { return GetPropertyList<TGCFolder>("ancestors"); } }
        #endregion

        #region Constructors and Initialization
        public TGCFolder()
            : base()
        {
            Initialize();
        }
        public TGCFolder(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            Initialize();
        }
        public TGCFolder(string apiPubKey)
            : base(apiPubKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            URI = BaseURI + "folder";
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a new folder with the given name for a user
        /// </summary>
        /// <param name="name">The name of the folder to create</param>
        /// <param name="session">The session used to create the folder</param>
        /// <param name="user">The user to create the folder for</param>
        /// <param name="parent">The parent folder to create the folder under</param>
        /// <returns>Returns the newly created folder</returns>
        public static TGCFolder CreateFolder(string name, TGCSession session, TGCUser user, TGCFolder parent)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", name),
                new TGCParameter("user_id", user.id),
                new TGCParameter("parent_id", parent.id)
            };

            var request = new TGCWebRequest(BaseURI + "folder", callParams);
            var response = request.Post();

            var newFolder = new TGCFolder(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            newFolder.rawresult = response.ResponseString;
            newFolder.Parse();
            return newFolder;
        }

        /// <summary>
        /// Updates a folder on the server
        /// </summary>
        /// <param name="session">The session used to update the folder</param>
        public void UpdateFolder(TGCSession session)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", name),
                new TGCParameter("user_id", user.id),
                new TGCParameter("parent_id", parent.id)
            };

            var request = new TGCWebRequest(BaseURI + "folder/" + id, callParams);
            //Not sure if null will work, but i'm not sure what i'm supposed to put in the PUT request
            var response = request.Put();
        }
        /// <summary>
        /// Deletes the folder on the server
        /// </summary>
        /// <param name="session">The session used to delete the folder</param>
        /// <returns>Returns success as boolean</returns>
        public bool DeleteFolder(TGCSession session)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id)
            };

            var request = new TGCWebRequest(URI + "/" + id, callParams);
            var response = request.Delete();

            var success = response.ResponseString.Contains("1");
            return success;
        }
        /// <summary>
        /// Fetches the folder with the given ID from the server
        /// </summary>
        /// <param name="id">The ID of the folder to get</param>
        /// <param name="session">Optional - The session to get the folder with</param>
        /// <returns>Returns the folder with the given ID</returns>
        public static TGCFolder FetchFolder(string id, TGCSession session = null)
        {
            var uri = BaseURI + "folder/" + id;
            TGCWebRequest request;
            TGCFolder folder;
            //var include_relationships = new TGCParameter("_include_relationships", "1");
            var includeRelationships = new TGCParameter("_include_relationships", "1");
            if (session != null)
            {
                request = new TGCWebRequest(uri, new TGCParameter("session", session.id), includeRelationships);
                folder = new TGCFolder(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            }
            else
            {
                request = new TGCWebRequest(uri, includeRelationships);
                folder = new TGCFolder();
            }
            var response = request.Get();
            folder.rawresult = response.ResponseString;           
            folder.Parse();
            return folder;
        }
        #endregion
    }
}
