using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using System.Globalization;

namespace Personal_Website.Projects.Minesweeper {
    /// <summary>
    /// Default (the only one) page.
    /// </summary>
    public partial class Default : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
			
		}

		/// <summary>
		/// Sets the culture to preferred one (from cookie).
		/// If user chooses another one, sets new cookie and change culture.
		/// </summary>
		protected override void InitializeCulture() {
			String selectedLanguage = "en-US"; // default
			HttpCookie cookieLang = new HttpCookie("preferedLanguage");

			// check, whether user wants to change the culture
			if (Request.Form["ukr_button"] != null) {
				selectedLanguage = "uk-ua";
			} else if (Request.Form["eng_button"] != null) {
				selectedLanguage = "en-US";
			} else if (Request.Cookies["preferedLanguage"] != null) { // if user has preferred culture, use it
				selectedLanguage = Request.Cookies["preferedLanguage"].Value;
			}
			
			// make cookie with selected culture	
			cookieLang.Value = selectedLanguage;
			cookieLang.Expires = DateTime.MaxValue;
			HttpContext.Current.Response.SetCookie(cookieLang);


			UICulture = selectedLanguage;
			Culture = selectedLanguage;

			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);

			base.InitializeCulture();
		}

		/// <summary>
		/// Opens specific place in the field.
		/// It may be sea, number or mine.
		/// </summary>
		/// <param name="x">X-coordinate of a place</param>
		/// <param name="y">Y-coordinate of a place</param>
		/// <returns>JSON string representing a list of changes to the field</returns>
        [WebMethod]
        public static string openPlace(int x, int y) {
			// if this is a first shot, generate field
			if (HttpContext.Current.Session["Field"] == null ) {
				HttpContext.Current.Session["Field"] = Field.initField(x, y, (Parameters)HttpContext.Current.Session["Parameters"]);			
			}

			// get list of changes, serialize it to JSON string and return it
            List<Change> changes = Field.openPlace(x, y);

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string json = jsonSerialiser.Serialize(changes);

            System.Diagnostics.Debug.Write(json+"\n");

            return json;
        }
		
		/// <summary>
		/// Returns a nickname of a player, if available.
		/// </summary>
		/// <returns>String, representing a nickname, or an empty string<./returns>
        [WebMethod]
        public static string getNickName() {
            return HttpContext.Current.Request.Cookies["UserID"] != null ? Utility.getNickname(Convert.ToInt32(HttpContext.Current.Request.Cookies["UserID"].Value)) : "";
        }

		/// <summary>
		/// Opens a number of places
		/// </summary>
		/// <param name="places">array of coordinates of places to open</param>
		/// <returns>List of changes to the field</returns>
		[WebMethod]
		public static string openPlaces(Coordinate[] places) {

			List<Change> changes = Field.openPlaces(places);

			JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
			string json = jsonSerialiser.Serialize(changes);

			return json;
		}

		/// <summary>
		/// Starts the game. Resets SESSION array.
		/// Generates or retrieves an ID of the player, add a record to DB that player has started new game.
		/// Updates nickname, if necessary. Does NOT generate field yet.
		/// </summary>
		/// <param name="width">width of a field</param>
		/// <param name="height">height of a field</param>
		/// <param name="minesNumber">number of mines in the field</param>
		/// <param name="userName">user's nickname</param>
		/// <param name="mode">game mode</param>
		/// <returns>True, if no exception was thrown.</returns>
        [WebMethod]
        public static bool startGame(int width, int height, int minesNumber, string userName, int mode) {
			HttpContext.Current.Session.Clear(); // clear SESSION array

			HttpContext.Current.Session["Parameters"] = new Parameters(width, height, minesNumber, mode); // save parameters for the game
			HttpContext.Current.Session["lost"] = false; // is game lost
			HttpContext.Current.Session["won"] = false; // is game won
			HttpContext.Current.Session["placesDiscovered"] = 0; // number of discovered places
			HttpContext.Current.Session["solverUsed"] = false; // true, if solver was used in the game

			// if person players for the first time, generate new ID and cookie for him
            if (HttpContext.Current.Request.Cookies["UserID"] == null) {
                HttpCookie cookieUserID = new HttpCookie("UserID");
                cookieUserID.Value = Utility.generateUserID();
				cookieUserID.Expires = DateTime.MaxValue;
                HttpContext.Current.Response.SetCookie(cookieUserID);

				// add new record to DB
                new Thread(delegate() {
                    Utility.addNickNameID(Convert.ToInt32(cookieUserID.Value), userName);
                }).Start();
            } else if (Default.getNickName() != userName) {	// if user wishes to change his nickname, let him do it
                int userIDtemp = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserID"].Value);

                new Thread(delegate() {
                    Utility.updateNickNameID(userIDtemp, userName);
                }).Start();
            }

			// remember ID in the SESSION
            int userID = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserID"].Value);
            HttpContext.Current.Session["UserID"] = userID;

			// add a record about the try
            new Thread(delegate() {
                Utility.addTryID(userID);
            }).Start();
            
            return true;
        }

		/// <summary>
		/// Checks, whether the game is won.
		/// </summary>
		/// <returns>True, if game is won, false otherwise.</returns>
        [WebMethod]
        public static bool isWon() {
			return HttpContext.Current.Session["won"] != null ? (bool)HttpContext.Current.Session["won"] : false;
        }

		/// <summary>
		/// Checks, whether the game is lost.
		/// </summary>
		/// <returns>True, if game is lost, false otherwise.</returns>
		[WebMethod]
		public static bool isLost() {
            return HttpContext.Current.Session["lost"] != null ? (bool)HttpContext.Current.Session["lost"] : false;
		}

		/// <summary>
		/// Checks, whether an instance of the game is currently running somewhere else in the user's browser.
		/// </summary>
		/// <returns>True, if game is running somewhere, false otherwise.</returns>
		[WebMethod]
		public static bool isGameRunning() {
			return HttpContext.Current.Session["Parameters"] != null;
		}

		/// <summary>
		/// Reveals all mines in the field.
		/// </summary>
		/// <returns>JSON string, containing serialized list of changes, where only mines are presented.</returns>
		[WebMethod]
		public static string getAllMines() {
            List<Change> changes = Field.getAllMines();

			JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
			string json = jsonSerialiser.Serialize(changes);

			System.Diagnostics.Debug.Write(json + "\n");

			return json;
		}
		  
		/// <summary>
		/// Returns leaders from DB for given mode.
		/// </summary>
		/// <param name="mode">game mode</param>
		/// <returns>JSON string, containing serialized list of leaders.</returns>
        [WebMethod]
        public static string getLeaderBoard(int mode)
        {
            List<getLeaderBoardResult> leaders = Utility.getLeaderBoard(mode);

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string json = jsonSerialiser.Serialize(leaders);

            System.Diagnostics.Debug.Write(json + "\n");

            return json;
        }

		[WebMethod]
		public static string runSolver(string json) {
			HttpContext.Current.Session["solverUsed"] = true;

			JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

			List<Move> result = Field.runSolver(json);


			string jsonAnswer = jsonSerialiser.Serialize(result);

			System.Diagnostics.Debug.Write(jsonAnswer + "\n");
			
			return jsonAnswer;
		}
    }
}