using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace CurrencyConverterAPI.Controllers
{
    public class CurrencyController : ApiController
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKey = "578f62f19720f763f244e24f"; 

        [HttpGet]
        [Route("api/currency/convert")]
        public async Task<IHttpActionResult> ConvertCurrency(string from, string to, double amount)
        {
            string apiUrl = $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/{from}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error fetching exchange rate.");
            }

            var data = JObject.Parse(await response.Content.ReadAsStringAsync());

            if (data["conversion_rates"]?[to] == null)
            {
                return BadRequest("Invalid currency, Please insert a CORRECT one.");
            }

            double exchangeRate = (double)data["conversion_rates"][to];
            double convertedAmount = amount * exchangeRate;

            return Ok(new { from, to, amount, convertedAmount });
        }
    }
}
