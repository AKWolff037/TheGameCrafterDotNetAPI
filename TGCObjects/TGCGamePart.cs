using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    public class TGCGamePart : TGCObjectBase
    {
        #region Public Properties
        /// <summary>
        /// The unique id for this gamepart. It will never change.
        /// </summary>
        public string id { get { return GetProperty("id") as string; } }
        /// <summary>
        /// gamepart.
        /// </summary>
        public string object_type { get { return GetProperty("object_type") as string; } }
        /// <summary>
        /// A date when the gamepart was created.
        /// </summary>
        public string date_created { get { return GetProperty("date_created") as string; } }
        /// <summary>
        /// A date when the gamepart was last updated.
        /// </summary>
        public string date_updated { get { return GetProperty("date_updated") as string; } }
        /// <summary>
        /// An integer between 1 and 99. Defaults to 1. Allows for multiple copies of a gamepart to be included in the game.
        /// </summary>
        public string quantity { get { return GetProperty("id") as string; } }
        /// <summary>
        /// The id of the game associated with this gamepart.
        /// </summary>
        public string game_id { get { return GetProperty("game_id") as string; } }
        /// <summary>
        /// The id of the part associated with this gamepart.
        /// </summary>
        public string part_id { get { return GetProperty("part_id") as string; } }
        /// <summary>
        /// A hash that is a brief representation of the part associated with this gamepart. It is provided so that when you get the list of gameparts for a game, you don't also have to make a call to Part.
        /// </summary>
        public TGCPart part { get { return GetProperty<TGCPart>("part"); } }
        /// <summary>
        /// The Game that uses this gamepart.
        /// </summary>
        public TGCGame game { get { return GetProperty<TGCGame>("game"); } }
        #endregion

        #region Constructors and Initialization
        public TGCGamePart()
            : base()
        {
            Initialize();
        }
        public TGCGamePart(string apiPubKey, string apiPrivKey)
            : base(apiPubKey, apiPrivKey)
        {
            Initialize();
        }
        public TGCGamePart(string apiPubKey)
            : base(apiPubKey)
        {
            Initialize();
        }
        private void Initialize()
        {
            URI = BaseURI + "gamepart";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a Game Part in a particular game
        /// </summary>
        /// <param name="session">The session to use</param>
        /// <param name="part">The part to add NOTE: Multiple parts of the same part_id are not allowed as separate part gamepart entries. Therefore if you try to create a new game part with a part_id that already exists in the game, it will update the existing gamepart and return that as the result.</param>
        /// <param name="game">The game to add the part to</param>
        /// <param name="quantity">The number of the part to add (default of 1)</param>
        /// <returns>Returns the newly created GamePart</returns>
        public static TGCGamePart CreateGamePart(TGCSession session, TGCPart part, TGCGame game, int quantity = 1)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("part_id", part.id),
                new TGCParameter("game_id", game.id),
                new TGCParameter("quantity", quantity.ToString())
            };

            var request = new TGCWebRequest(BaseURI + "gamepart", callParams);
            var response = request.Post();

            var newGamePart = new TGCGamePart(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            newGamePart.rawresult = response.ResponseString;
            newGamePart.Parse();
            return newGamePart;
        }

        public void Update(TGCSession session)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("part_id", part.id),
                new TGCParameter("game_id", game.id),
                new TGCParameter("quantity", quantity.ToString())
            };

            var request = new TGCWebRequest(BaseURI + "gamepart/" + id, callParams);
            var response = request.Put();

            this.rawresult = response.ResponseString;
            this.Parse();
        }

        public bool Delete(TGCSession session)
        {
            var callParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id)
            };
            var request = new TGCWebRequest(BaseURI + "gamepart/" + id, callParams);
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
        public static TGCGamePart FetchGamePart(string id, TGCSession session = null)
        {
            var uri = BaseURI + "gamepart/" + id;
            TGCWebRequest request;
            TGCGamePart gamePart;
            var includeRelationships = new TGCParameter("_include_relationships", "1");
            if (session != null)
            {
                request = new TGCWebRequest(uri, new TGCParameter("session", session.id), includeRelationships);
                gamePart = new TGCGamePart(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            }
            else
            {
                request = new TGCWebRequest(uri, includeRelationships);
                gamePart = new TGCGamePart();
            }
            var response = request.Get();
            gamePart.rawresult = response.ResponseString;
            gamePart.Parse();
            return gamePart;
        }

        #endregion
    }
}
