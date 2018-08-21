using Cards.Client;
using Cards.Client.Models;
using Gateway.Configuration;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/cards")]
    public class CardsController : Controller
    {
        CardsClient _client;

        public CardsController(QueueingService queueing)
        {
            _client = new CardsClient(queueing.ConnectionFactory);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string[] cardIds)
        {
            var request = new GetCardsRequest() { CardIds = cardIds };
            var reply = await _client.GetCards(request, Timeouts.GLOBAL);
            if (!reply.Success) { return StatusCode(500); }
            return Ok(reply.Response);
        }
    }
}