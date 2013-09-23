using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    public class TGCReview : TGCObjectBase
    {
        #region Public Properties
        /// <summary>
        /// The unique id for this review. It will never change.
        /// </summary>
        public string id { get { return GetProperty("id") as string; } }
        /// <summary>
        /// review.
        /// </summary>
        public string object_type { get { return GetProperty("object_type") as string; } }
        /// <summary>
        /// A date when the review was created.
        /// </summary>
        public string date_created { get { return GetProperty("date_created") as string; } }
        /// <summary>
        /// A date when the review was last updated.
        /// </summary>
        public string date_updated { get { return GetProperty("date_updated") as string; } }
        /// <summary>
        /// A block of text, not to exceed 65000 characters.
        /// </summary>
        public string review { get { return GetProperty("review") as string; } }
        /// <summary>
        /// An integer between 1 and 5, 5 being best.
        /// </summary>
        public string rating { get { return GetProperty("rating") as string; } }
        /// <summary>
        /// The unique id of a Game that this review is about.
        /// </summary>
        public string game_id { get { return GetProperty("game_id") as string; } }
        /// <summary>
        /// The unique id of a User that created this review.
        /// </summary>
        public string user_id { get { return GetProperty("user_id") as string; } }
        /// <summary>
        /// The User that this review is created by.
        /// </summary>
        public TGCUser user { get { return GetProperty<TGCUser>("user"); } }
        /// <summary>
        /// The Game that this review is about.
        /// </summary>
        public TGCGame game { get { return GetProperty<TGCGame>("game"); } }
        #endregion

        #region Constructors
        public TGCReview()
            : base()
        {
            Initialize();
        }
        public TGCReview(string pubAPIKey)
            : base(pubAPIKey)
        {
            Initialize();
        }
        public TGCReview(string pubKey, string privKey)
            : base(pubKey, privKey)
        {
            Initialize();
        }

        public void Initialize()
        {
            URI = BaseURI + "/review";
        }
        #endregion

        #region Methods
        //TODO
        #endregion
    }
}
