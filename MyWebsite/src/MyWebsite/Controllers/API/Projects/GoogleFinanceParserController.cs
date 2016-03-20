using System;
using Microsoft.AspNet.Mvc;
using MyWebsite.ViewModels.Projects.GoogleFinanceParser;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyWebsite.Controllers.API.Projects
{
	[Produces("application/json")]
	[Route("api/Projects/GoogleFinanceParser")]
	public class GoogleFinanceParserController : Controller
	{

		// GET: api/Projects/GoogleFinanceParser
		[HttpGet]
		public async Task<SymbolStrike> GetGoogleFinanceParser(GoogleFinanceParserViewModel model)
		{
			try
			{
				var httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyClient", "1.0"));
				var json = await httpClient.GetStringAsync($"http://www.google.com/finance/option_chain?q={model.symbol}&output=json");

				json = cleanJSON(json);

				dynamic result = JsonConvert.DeserializeObject(json);

				foreach (var item in result)
				{

					if (float.Parse(((object)item.strike).ToString().Replace("\"", "")) == model.strike)
					{
						return new SymbolStrike()
						{
							symbol = model.symbol,
							strike = model.strike,
							bid = Convert.ToDouble(item.b),
							ask = Convert.ToDouble(item.a)
						};
					}
				}

			}
			catch (Exception)
			{

			}

			return new SymbolStrike()
			{
				symbol = model.symbol,
				strike = model.strike,
				bid = -1,
				ask = -1
			};
		}

		private string cleanJSON(string json)
		{

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

	public struct SymbolStrike
	{
		public string symbol;
		public double strike;
		public double bid;
		public double ask;
	}
}

