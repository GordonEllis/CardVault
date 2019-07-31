using Cards.Client;
using Cards.Client.Models;
using Gateway.Configuration;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LocalData.Client;
using LocalData.Client.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ImageUri = Cards.Client.Models.ImageUri;
using Newtonsoft.Json;

namespace Gateway.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/cards")]
    public class CardsController : Controller
    {
        CardsClient _client;
        LocalDataClient _localdataClient;

        public CardsController(QueueingService queueing)
        {
            _client = new CardsClient(queueing.ConnectionFactory);
            _localdataClient = new LocalDataClient(queueing.ConnectionFactory);
        }

		//get all cards saved to heroku database (or specific ones)
		[HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCards(string[] cardIds)
        {
            var request = new GetCardsRequest() { CardIds = cardIds };
            var reply = await _client.GetCards(request, Timeouts.GLOBAL);

            if (!reply.Success) { return StatusCode(500); }
            return Ok(reply.Response);
        }

        //[HttpPost]
        //[Route("")]
        //public async Task<IActionResult> SaveCards(Card[] cards)
        //{
        //    var request = new SaveCardsRequest() { CardData = cards };
        //    var reply = await _client.SaveCards(request, Timeouts.GLOBAL);
        //    return Ok(reply.Response);
        //}

		//Lookup spreadsheet and save to the heroku database
        [HttpGet]
        [Route("localdata")]
        public async Task<IActionResult> SaveSpreadsheetCards()
        {
            var cards = await _localdataClient.LoadDataFromSpreadsheet(new LoadCardDataRequest(), Timeouts.GLOBAL);
            var request = new SaveCardsRequest() { CardData = Convert(cards.Response.Item1) };
            var reply = await _client.SaveCards(request, Timeouts.GLOBAL);
            return Ok(reply.Response);
        }

        private Card[] Convert(CollectionData[] data)
        {
            List<Card> target = new List<Card>();

            foreach (CollectionData item in data)
            {
                try
                {
					Card newCard = new Card();
                    newCard.Id = item.Id;
                    newCard.Name = item.Name;
					newCard.ImageUris = item.ImageUris.ToObject<ImageUri>();
					 newCard.ImageUris.Id = item.Id;
					newCard.ManaCost = item.ManaCost;
                    newCard.ConvertedManaCost = item.Cmc == null ? 0 : (int)item.Cmc;
                    newCard.Type = item.TypeLine;
                    newCard.Text = item.OracleText;
                    newCard.ColorIdentity = item.Colors == null ? "0" : item.Colors.ToString();
                    newCard.Set = item.Set;
                    newCard.SetName = item.SetName;
                    newCard.Rarity = item.Rarity;
                    newCard.Value = 0;
                    newCard.Quantity = item.Quantity;

                    var alreadyExists = target.Find(i => i.Id == newCard.Id);
                    if (alreadyExists == null)
                        target.Add(newCard);
                    else
                        alreadyExists.Quantity += newCard.Quantity;
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex + " Something went wrong " + item);
                }

            }
            return target.ToArray();
        }
    }
}