using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    [Serializable]
    public class TGCGame : TGCObjectBase
    {
        #region Public Properties
        /// <summary>
        /// The unique id for this game. It will never change.
        /// </summary>
        public string id { get { return GetProperty("id") as string; } }
        /// <summary>
        /// game.
        /// </summary>
        public string object_type { get { return GetProperty("object_type") as string; } }
        /// <summary>
        /// A date when the game was created.
        /// </summary>
        public string date_created { get { return GetProperty("date_created") as string; } }
        /// <summary>
        /// A date when the game was last updated.
        /// </summary>
        public string date_updated { get { return GetProperty("date_updated") as string; } }
        /// <summary>
        /// The name the game is known by.
        /// Setting this will also set the shop URI for the game. A game name like Ninjas: Kill Or Be Killed!!! will generate a URI like ninjas-kill-or-be-killed. It should also be said that Kill Or Be Killed probably belongs in the short_description field, unless it is part of the game name.
        /// </summary>
        public string name { get { return GetProperty("name") as string; } }
        /// <summary>
        /// The unique id of a designer that has control over the game.
        /// </summary>
        public string designer_id { get { return GetProperty("designer_id") as string; } }
        /// <summary>
        /// A brief text description of the game.
        /// </summary>
        public string description { get { return GetProperty("description") as string; } }
        /// <summary>
        /// The description field formatted as HTML.
        /// </summary>
        public string description_html { get { return GetProperty("description_html") as string; } }
        /// <summary>
        /// A short phrase to describe the game, like you'd put on a movie poster.
        /// </summary>
        public string short_description { get { return GetProperty("short_description") as string; } }
        /// <summary>
        /// A boolean indicating whether this game has been featured on Facebook yet or not.
        /// </summary>
        public string has_promoed_facebook { get { return GetProperty("has_promoed_facebook") as string; } }
        /// <summary>
        /// A boolean indicating whether this game has been featured on Twitter yet or not.
        /// </summary>
        public string has_promoed_twitter { get { return GetProperty("has_promoed_twitter") as string; } }
        /// <summary>
        /// A boolean indicating whether this game has been featured in TGC's featured section yet or not.
        /// </summary>
        public string has_promoed_featured { get { return GetProperty("has_promoed_featured") as string; } }
        /// <summary>
        /// The URI for the web site for this specific game, or the product page on the designer's web site for this specific game.
        /// </summary>
        public string website_uri { get { return GetProperty("website_uri") as string; } }
        /// <summary>
        /// If you could tell a potential buyer just one thing about your game to make them want to buy it, this is the place you'd put that.
        /// </summary>
        public string cool_factor_1 { get { return GetProperty("cool_factor_1") as string; } }
        /// <summary>
        /// If you could tell a potential buyer a second thing about your game to make them want to buy it, this is the place you'd put that.
        /// </summary>
        public string cool_factor_2 { get { return GetProperty("cool_factor_2") as string; } }
        /// <summary>
        /// If you were lucky enough to be able to tell a potential buyer a third reason they should buy your game, this is where you'd put that.
        /// </summary>
        public string cool_factor_3 { get { return GetProperty("cool_factor_3") as string; } }
        /// <summary>
        /// A boolean indicating whether the game is ready to be sold in TGC's shop.
        /// </summary>
        public string public_ind { get { return GetProperty("public") as string; } }
        /// <summary>
        /// A date when the game was first made public in TGC's shop.
        /// </summary>
        public string date_published { get { return GetProperty("date_published") as string; } }
        /// <summary>
        /// Usually "First" or "1". Some indication of the version number of the game in case you've made changes.
        /// </summary>
        public string edition { get { return GetProperty("edition") as string; } }
        /// <summary>
        /// The minimum age to play the game. See Game Options below for details.
        /// </summary>
        public string min_age { get { return GetProperty("min_age") as string; } }
        /// <summary>
        /// The minimum number of players required to play the game. Default 1. Max 99.
        /// </summary>
        public string min_players { get { return GetProperty("min_players") as string; } }
        /// <summary>
        /// The maximum number of players that can play the game. Default 99. Min see min_players.
        /// </summary>
        public string max_players { get { return GetProperty("max_players") as string; } }
        /// <summary>
        /// The average amount of minutes it takes to play the game. See Game Options below for details.
        /// </summary>
        public string play_time { get { return GetProperty("play_time") as string; } }
        /// <summary>
        /// An integer between 1 and 5 representing the average rating of a game across all reviews and ratings.
        /// </summary>
        public string average_rating { get { return GetProperty("average_rating") as string; } }
        /// <summary>
        /// A believability score for the average_rating based upon the number of ratings it has received compared to other games in the system.
        /// </summary>
        public string bayesian_ranking { get { return GetProperty("bayesian_ranking") as string; } }
        /// <summary>
        /// An integer which represents how many 5 star ratings have been made of this game.
        /// </summary>
        public string ratings_at_5 { get { return GetProperty("ratings_at_5") as string; } }
        /// <summary>
        /// An integer which represents how many 4 star ratings have been made of this game.
        /// </summary>
        public string ratings_at_4 { get { return GetProperty("ratings_at_4") as string; } }
        /// <summary>
        /// An integer which represents how many 3 star ratings have been made of this game
        /// </summary>
        public string ratings_at_3 { get { return GetProperty("ratings_at_3") as string; } }
        /// <summary>
        /// An integer which represents how many 2 star ratings have been made of this game.
        /// </summary>
        public string ratings_at_2 { get { return GetProperty("ratings_at_2") as string; } }
        /// <summary>
        /// An integer which represents how many 1 star ratings have been made of this game.
        /// </summary>
        public string ratings_at_1 { get { return GetProperty("ratings_at_1") as string; } }
        /// <summary>
        /// An overall category for the game. See Game Options for details.
        /// </summary>
        public string genre { get { return GetProperty("genre") as string; } }
        /// <summary>
        /// What gives the game its flavor? See Game Options for details.
        /// </summary>
        public string theme { get { return GetProperty("theme") as string; } }
        /// <summary>
        /// What is the general setup of the game? See Game Options for details.
        /// </summary>
        public string game_type { get { return GetProperty("game_type") as string; } }
        /// <summary>
        /// Who are the intended players of the game? See Game Options for details.
        /// </summary>
        public string audience { get { return GetProperty("audience") as string; } }
        /// <summary>
        /// What is the primary mechanic used in the game? See Game Options for details.
        /// </summary>
        public string primary_mechanic { get { return GetProperty("primary_mechanic") as string; } }
        /// <summary>
        /// The manufacturer's suggested retail price of the game as set by the Designer. Must be an integer between 0 and 99. All prices automatically have .99 tacked on to them in the shop.
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
        /// The minimum cost to produce one copy of the game, based upon all the components it has in it.
        /// </summary>
        public string cost { get { return GetProperty("cost") as string; } }
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
        /// A boolean indicating whether this game is picked as one of the TGC staff's favorites.
        /// </summary>
        public string staff_pick { get { return GetProperty("staff_pick") as string; } }
        /// <summary>
        /// A boolean indicating whether this game is marked as a featured product in the shop.
        /// </summary>
        public string featured { get { return GetProperty("featured") as string; } }
        /// <summary>
        /// A date indicating when the featured status of the game will expire.
        /// </summary>
        public string featured_expires { get { return GetProperty("featured_expires") as string; } }
        /// <summary>
        /// The URI to this game in the TGC shop.
        /// </summary>
        public string shop_uri { get { return GetProperty("shop_uri") as string; } }
        /// <summary>
        /// The unique id for this game on Board Game Geek (http://www.boardgamegeek.com).
        /// </summary>
        public string bgg_id { get { return GetProperty("bgg_id") as string; } }
        /// <summary>
        /// Sets the image used at the top of the shop page. The ID is from a File. If a backdrop_id is specified, then an extra property called backdrop will be added which will briefly describe the image.
        /// </summary>
        public string backdrop_id { get { return GetProperty("backdrop_id") as string; } }
        /// <summary>
        /// Briefly describes the image in the backdrop
        /// </summary>
        public string backdrop { get { return GetProperty("backdrop") as string; } }
        /// <summary>
        /// Sets the image used inline in the shop page. The ID is from a File. If a logo_id is specified, then an extra property called logo will be added which will briefly describe the image.
        /// </summary>
        public string logo_id { get { return GetProperty("logo_id") as string; } }
        /// <summary>
        /// Briefly describes the image in the logo
        /// </summary>
        public string logo { get { return GetProperty("logo") as string; } }
        /// <summary>
        /// Sets the image used to advertise the game on the site. The ID is from a File. If a advertisement_id is specified, then an extra property called advertisement will be added which will briefly describe the image.
        /// </summary>
        public string advertisement_id { get { return GetProperty("advertisement_id") as string; } }
        /// <summary>
        /// Briefly describes the iamge in the advertisement
        /// </summary>
        public string advertisement { get { return GetProperty("advertisement") as string; } }
        /// <summary>
        /// The unique id of the Review that the current user, as identified by the session_id, has posted about this game. If no session_id is specified in the request, or the user has not posted a review, then nothing will be listed here.
        /// </summary>
        public string my_review_id { get { return GetProperty("my_review_id") as string; } }
        
        //TODO : RELATIONSHIPS
        //pokerdecks
        //tokenstickers
        //bigmats
        //skinnymats
        //smallsquaremats
        //largesquaremats
        //spinnermats
        //boards
        //documents
        //gameparts
        //pokertuckbox36
        //pokertuckbox54
        //pokertuckbox72
        //pokertuckbox90
        //pokertuckbox108
        //designer
        //actionshots
        //reviews
        //my_review
        //similar

        #endregion

        #region Constructors and Initialization
        public TGCGame()
            : base()
        {
            Initialize();
        }
        public TGCGame(string pubAPIKey)
            : base(pubAPIKey)
        {
            Initialize();
        }
        public TGCGame(string pubKey, string privKey)
            : base(pubKey, privKey)
        {
            Initialize();
        }

        public void Initialize()
        {
            URI = BaseURI + "/game";
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a game on the TGC server under the given designer
        /// </summary>
        /// <param name="gameName">The name of the game to create</param>
        /// <param name="designer">The designer to create the game under</param>
        /// <param name="session"></param>
        /// <param name="optionalParams">See: <see cref="https://www.thegamecrafter.com/developer/Game.html" /> for details</param>
        /// <returns>Returns the newly created game</returns>
        public static TGCGame CreateGame(string gameName, TGCDesigner designer, TGCSession session, params TGCParameter[] optionalParams)
        {
            var requiredParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", gameName),
                new TGCParameter("designer_id", designer.id)
            };
            var callParams = requiredParams.ToList();
            callParams.AddRange(optionalParams);
            var request = new TGCWebRequest(BaseURI + "game", callParams.ToArray());
            var response = request.Post();

            var newGame = new TGCGame(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            newGame.rawresult = response.ResponseString;
            newGame.Parse();
            return newGame;
        }

        public void UpdateGame(TGCSession session)
        {
            var requiredParams = new TGCParameter[]
            {
                new TGCParameter("session_id", session.id),
                new TGCParameter("name", name),
                new TGCParameter("designer_id", designer_id)
            };

            var optionalParams = TGCParameter.CreateParameterList(result, "description", "short_description", "website_uri", "cool_factor_1", "cool_factor_2", "cool_factor_3", "edition",
                                                                          "min_age", "min_players", "max_players", "play_time", "genre", "theme", "setting", "game_type", "audience", "primary_mechanic",
                                                                          "msrp", "msrp_10", "msrp_100", "msrp_1000", "bgg_id", "backdrop_id", "advertisement_id", "logo_id");
            optionalParams.AddRange(requiredParams);
            var request = new TGCWebRequest(this.URI + "/" + this.id, optionalParams.ToArray());
            var response = request.Put();
            this.rawresult = response.ResponseString;
            this.Parse();
        }

        /// <summary>
        /// Deletes the game on the server
        /// </summary>
        /// <param name="session">The session used to delete the game</param>
        /// <returns>Returns success as boolean</returns>
        public bool Delete(TGCSession session)
        {
            var request = new TGCWebRequest(this.URI + "/" + this.id, new TGCParameter("session_id", session.id));
            var response = request.Delete();

            var success = response.ResponseString.Contains("1");
            return success;
        }

        /// <summary>
        /// Fetches the game with the given ID from the server
        /// </summary>
        /// <param name="id">The ID of the game to get</param>
        /// <param name="session">Optional - The session to get the game with</param>
        /// <returns>Returns the game with the given ID</returns>
        public static TGCGame FetchDesigner(string id, TGCSession session = null)
        {
            var uri = BaseURI + "game/" + id;
            TGCWebRequest request;
            TGCGame game;
            var includeRelationships = new TGCParameter("_include_relationships", "1");
            if (session != null)
            {
                request = new TGCWebRequest(uri, new TGCParameter("session", session.id), includeRelationships);
                game = new TGCGame(session.API_PUBLIC_KEY, session.API_PRIVATE_KEY);
            }
            else
            {
                request = new TGCWebRequest(uri, includeRelationships);
                game = new TGCGame();
            }
            var response = request.Get();
            game.rawresult = response.ResponseString;
            game.Parse();
            return game;
        }

        //TODO : Add/Update Review
        //Copy Game
        //Publish Game
        //Unpublish Game
        //Similar Games
        //Search All Games
        //Bulk Pricing
        #endregion
    }
}
