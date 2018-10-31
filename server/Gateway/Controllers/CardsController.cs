using Cards.Client;
using Cards.Client.Models;
using Gateway.Configuration;
using Gateway.Services;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
=======
using LocalData.Client;
using LocalData.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
>>>>>>> major-updates
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/cards")]
    public class CardsController : Controller
    {
        CardsClient _client;
        LocalDataClient _client2;

        public CardsController(QueueingService queueing)
        {
            _client = new CardsClient(queueing.ConnectionFactory);
            _client2 = new LocalDataClient(queueing.ConnectionFactory);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string[] cardIds)
        {
            var request = new GetCardsRequest() { CardIds = cardIds };
            var reply = await _client.GetCards(request, Timeouts.GLOBAL);

            var request2 = new LoadCardDataRequest() { CardNames = cardIds };
			var reply2 = await _client2.GetCollectionData(request2, Timeouts.GLOBAL);
			
            if (!reply.Success) { return StatusCode(500); }
            return Ok(reply2.Response);
        }
    }
}