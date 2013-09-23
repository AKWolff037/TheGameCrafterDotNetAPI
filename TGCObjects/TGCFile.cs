using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// This class represents a file on the TGC servers
    /// </summary>
    [Serializable]
    public class TGCFile : TGCObjectBase
    {

        #region Public Properties
        /// <summary>
        /// The unique id for this file. It will never change.
        /// </summary>
        public string id
        {
            get { return GetProperty("id") as string; }
        }
        /// <summary>
        /// file
        /// </summary>
        public string wing_object_type
        {
            get { return GetProperty("wing_object_type") as string; }
        }
        /// <summary>
        /// A date when the file was created.
        /// </summary>
        public string date_created
        {
            get { return GetProperty("date_created") as string; }
        }
        /// <summary>
        /// A date when the file was last updated.
        /// </summary>
        public string date_updated
        {
            get { return GetProperty("date_updated") as string; }
        }
        /// <summary>
        /// The name the file is known by
        /// </summary>
        public string name
        {
            get { return GetProperty("name") as string; }
        }
        /// <summary>
        /// A unique hash of the file. This is useful when you want to cache files locally, or avoid uploading a file you've already uploaded, because it is always unique to a file
        /// </summary>
        public string sha1_digest
        {
            get { return GetProperty("sha1_digest") as string; }
        }
        /// <summary>
        /// The unique id of a Folder that this file is contained in.
        /// </summary>
        public string folder_id
        {
            get { return GetProperty("folder_id") as string; }
        }
        /// <summary>
        /// The file's size in bytes.
        /// </summary>
        public string file_size
        {
            get { return GetProperty("file_size") as string; }
        }
        /// <summary>
        /// A string representing what kind of file this is
        /// </summary>
        public string file_type
        {
            get { return GetProperty("file_type") as string; }
        }
        /// <summary>
        /// A string describing the file. If it's a PDF it will be how many pages it contains. If it's an image it will give some resolution information.
        /// </summary>
        public string details
        {
            get { return GetProperty("details") as string; }
        }
        /// <summary>
        /// Either Ready or Processing. This indicates wehther the file has been processed for inclusion into The Game Crafter's file handling system. Things like generating a preview image and creating a permanent storage location are done while processing.
        /// </summary>
        public string status
        {
            get { return GetProperty("status") as string; }
        }
        /// <summary>
        /// A URI to an image that shows a small representation of the original image or the first page of a PDF document.
        /// </summary>
        public string preview_uri
        {
            get { return GetProperty("preview_uri") as string; }
        }
        /// <summary>
        /// A URI to the permanent storage location of the file.
        /// </summary>
        public string file_uri
        {
            get { return GetProperty("file_uri") as string; }
        }
        /// <summary>
        /// The Folder that this file is contained in.
        /// </summary>
        public TGCFolder folder
        {
            get { return GetProperty<TGCFolder>("folder"); }
        }
        #endregion

        #region Constructors and Initialization
        public TGCFile()
            : base()
        {
            Initialize();
        }
        public TGCFile(string pubAPIKey)
            : base(pubAPIKey)
        {
            Initialize();
        }
        public TGCFile(string pubKey, string privKey)
            : base(pubKey, privKey)
        {
            Initialize();
        }

        public void Initialize()
        {
            URI = BaseURI + "/file";
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creates a file on the server
        /// </summary>
        /// <param name="session">The session to use</param>
        /// <param name="fileName">The name of the file to use on TGC's servers</param>
        /// <param name="file">The file as a byte array</param>
        /// <param name="rootFolder">The folder that the file should be placed into</param>
        /// <returns>Returns the created file</returns>
        public static TGCFile CreateFile(TGCSession session, string fileName, byte[] file, TGCFolder rootFolder)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", fileName),
                new TGCParameter(fileName, file),
                new TGCParameter("folder_id", rootFolder.id)
            };

            var request = new TGCWebRequest(BaseURI + "file", callParams);
            var response = request.Post();

            var newFile = new TGCFile(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            newFile.rawresult = response.ResponseString;
            newFile.Parse();
            return newFile;
        }
        /// <summary>
        /// Updates a file on the server
        /// </summary>
        /// <param name="session">The session to use</param>
        /// <param name="fileNameOnTGC">The new file name on TGC's servers</param>
        /// <param name="newFile">The file as a byte array - pass null if no new file to upload</param>
        /// <param name="rootFolder">The folder that the file should be placed into</param>
        public void UpdateFile(TGCSession session, string fileName, byte[] newFile, TGCFolder rootFolder)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", fileName),
                new TGCParameter(fileName, newFile),
                new TGCParameter("folder_id", rootFolder.id)
            };

            var request = new TGCWebRequest(BaseURI + "file/" + this.id, callParams);
            var response = request.Put();
        }
        /// <summary>
        /// Deletes a file from TGC's servers
        /// </summary>
        /// <param name="session">The session to use</param>
        /// <returns>Returns a boolean indicating success</returns>
        public bool DeleteFile(TGCSession session)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id)
            };

            var request = new TGCWebRequest(BaseURI + "file/" + this.id, callParams);
            var response = request.Delete();

            var success = response.ResponseString.Contains("1");
            return success;
        }
        /// <summary>
        /// Fetches a file's details
        /// </summary>
        /// <param name="id">The ID of the file to get details for</param>
        /// <param name="session">The session to use (optional)</param>
        /// <returns>Returns the file with details</returns>
        public static TGCFile FetchFile(string id, TGCSession session = null)
        {
            var uri = BaseURI + "file/" + id;
            TGCWebRequest request;
            TGCFile file;
            TGCParameter includeRelated = new TGCParameter("_include_relationships", "1");
            TGCParameter includeObjects = new TGCParameter("_include_related_objects", "1");
            if (session != null)
            {
                request = new TGCWebRequest(uri, new TGCParameter("session", session.id), includeRelated, includeObjects);
                file = new TGCFile(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            }
            else
            {
                request = new TGCWebRequest(uri);
                file = new TGCFile();
            }
            var response = request.Get();
            file.rawresult = response.ResponseString;
            file.Parse();

            return file;
        }
        #endregion
    }
}
