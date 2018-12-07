using LocalData.Client;
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
		[Route("")]
		public async Task<IActionResult> LoadDataFromSpreadsheet()
		{
			var request = new LoadCardDataRequest();
			var reply = await _client.LoadDataFromSpreadsheet(request, Timeouts.GLOBAL);
			if (!reply.Success) { return StatusCode(500); }
			return Ok(reply.Response);
		}
	}
}