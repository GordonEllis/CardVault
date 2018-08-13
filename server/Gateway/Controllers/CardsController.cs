using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gateway.Services;
using Cards.Models;
using System.Threading.Tasks;
using Cards;
using Gateway.Configuration;

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