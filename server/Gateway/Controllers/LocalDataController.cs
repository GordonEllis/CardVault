using LocalData.Client;
using Cards.Client.Models;
using Gateway.Configuration;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LocalData.Client.Models;

namespace Gateway.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/localData")]
    public class LocalDataController : Controller
    {
        LocalDataClient _client;

        public LocalDataController(QueueingService queueing)
        {
            _client = new LocalDataClient(queueing.ConnectionFactory);
        }

		[HttpGet]
		[Route("collection")]
		public async Task<IActionResult> GetCollectionData(string[] cardNames)
		{
			var request = new LoadCardDataRequest() { CardNames = cardNames };
			var reply = await _client.GetCollectionData(request, Timeouts.GLOBAL);
			if (!reply.Success) { return StatusCode(500); }
			return Ok(reply.Response);
		}

		[HttpGet]
		[Route("localcard")]
		public async Task<IActionResult> GetLocalCardData(string[] cardNames)
        {
            var request = new LoadCardDataRequest() { CardNames = cardNames };
            var reply = await _client.GetLocalData(request, Timeouts.GLOBAL);
            if (!reply.Success) { return StatusCode(500); }
            return Ok(reply.Response);
        }

		[HttpGet]
		[Route("scryfall")]
		public async Task<IActionResult> GetScryfallData(string[] cardNames)
		{
			var request = new LoadScryfallDataRequest() { CardNames = cardNames };
			var reply = await _client.GetLocalScryfallData(request, Timeouts.GLOBAL);
			if (!reply.Success) { return StatusCode(500); }
			return Ok(reply.Response);
		}
	}
}