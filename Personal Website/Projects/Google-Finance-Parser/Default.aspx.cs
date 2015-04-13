﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text.RegularExpressions;
using System.Json;

namespace Personal_Website.Projects.Yahoo_Finance_Parser {

	struct SymbolStrike {
		public string symbol;
		public string strike;
		public string bid;
		public string ask;
	}

	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

				var symbol = Symbol.Text;
				var strike = Strike.Text;

				if (symbol == "" || strike == "") {
					symbol = Request.QueryString["symbol"];
					strike = Request.QueryString["strike"];
				}

				if (symbol == null || strike == null) {
					symbol = "AAPL";
					strike = "128";
				} if (symbol[0] == '{') {

					var symbols = symbol.Substring(1, symbol.Length - 2).Split(' ');
					var strikes = strike.Substring(1, strike.Length - 2).Split(' ');

					List<SymbolStrike> input = new List<SymbolStrike>();

					for (int i = 0; i < symbols.Length; i++) {
						input.Add(new SymbolStrike() {
							symbol = symbols[i],
							strike = strikes[i]
						});
					}

					computeOutput(input.ToArray());

					return;
				}
				
				computeOutput( new SymbolStrike() {
					symbol = symbol,
					strike = strike
				});
		}

		private void setCookie(params SymbolStrike[] args) {

			var sym = "symbol={";
			var str = "strike={";
			var url = "Default.aspx?";

			foreach (var item in args) {
				sym += "+" + item.symbol;
				str += "+" + item.strike;
			}

			sym += "}";
			str += "}";

			url += sym + "&" + str;
			
			HttpCookie cookieUser = new HttpCookie("parserCookie");
			cookieUser.Value = url; //HttpContext.Current.Request.Url.AbsoluteUri;
			cookieUser.Expires = DateTime.MaxValue;
			HttpContext.Current.Response.SetCookie(cookieUser);
		}

		private void computeOutput(params SymbolStrike[] args) {

			setCookie(args);

			List<SymbolStrike> resultList = new List<SymbolStrike>();

			foreach (var arg in args) {

				try {

					var json = new WebClient().DownloadString("http://www.google.com/finance/option_chain?q=" + arg.symbol + "&output=json");

					json = cleanJSON(json);

					dynamic result = JsonValue.Parse(json);

					foreach (var item in result) {

						if ( float.Parse(((object)item.strike).ToString().Replace("\"", "")) == float.Parse(arg.strike)) {
							resultList.Add(new SymbolStrike() {
								symbol = arg.symbol,
								strike = arg.strike,
								bid = item.b,
								ask = item.a
							});
							break;
						}
					}					

				} catch (Exception) {
					
				}

			}

			resultingTable.InnerHtml = getHTMLTable(resultList.ToArray());

		}

		private string getHTMLTable(params SymbolStrike[] args) {

			var result = "";

			foreach (var arg in args) {
				result += "<tr class='success'>";
				result += "<th>" + arg.symbol + "</th>";
				result += "<th>" + arg.strike + "</th>";
				result += "<th>" + arg.bid + "</th>";
				result += "<th>" + arg.ask + "</th>";
				result += "</tr>";
			}

			return result;
		}

		private string cleanJSON(string json) {

			json = json.Substring(json.IndexOf("calls:") + 6, json.IndexOf(",underlying_id") - 6 - json.IndexOf("calls:"));

			json = json.Replace("expiry", "\"expiry\"");
			json = json.Replace("cid", "\"cid\"");
			json = json.Replace("name", "\"name\"");
			json = json.Replace(",s:", ",\"s\":");
			json = json.Replace(",e:", ",\"e\":");
			json = json.Replace(",p:", ",\"p\":");
			json = json.Replace(",c:", ",\"c\":");
			json = json.Replace(",b:", ",\"b\":");
			json = json.Replace(",a:", ",\"a\":");
			json = json.Replace(",oi:", ",\"oi\":");
			json = json.Replace(",cs:", ",\"cs\":");
			json = json.Replace(",cp:", ",\"cp\":");
			json = json.Replace("vol", "\"vol\"");
			json = json.Replace("strike", "\"strike\"");

			return json;
		}
	}
}