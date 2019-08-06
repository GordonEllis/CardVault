using Gateway.Configuration;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Decks.Client;
using Decks.Client.Models;

namespace Gateway.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/decks")]
    public class DecksController : Controller
    {
        DecksClient _client;
        
        public DecksController(QueueingService queueing)
        {
            _client = new DecksClient(queueing.ConnectionFactory);
        }

		//get all decks saved to heroku database (or specific ones)
		[HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDecks(int[] deckIds)
        {
            var request = new GetDecksRequest() { DeckIds = deckIds };
            var reply = await _client.GetDecks(request, Timeouts.GLOBAL);

            if (!reply.Success) { return StatusCode(500); }
            return Ok(reply.Response);
        }

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> SaveDeck([FromBody] Deck deck)
		{
			var request = new SaveDeckRequest() { DeckData = deck };
			var reply = await _client.SaveDeck(request, Timeouts.GLOBAL);

			if (!reply.Success) { return StatusCode(500); }
			return Ok(reply.Response);
		}

		//[HttpDelete]
		//[Route("")]
		//public async Task<IActionResult> DeleteDeck([FromBody] int[] deckId)
		//{
		//	var request = new DeleteDeckRequest() { DeckIds = deckId };
		//	var reply = await _client.DeleteDeck(request, Timeouts.GLOBAL);

		//	if (!reply.Success) { return StatusCode(500); }
		//	return Ok(reply.Response);
		//}
	}
}