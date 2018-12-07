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
        [Route("")]
        public async Task<IActionResult> GetCards(string[] cardIds)
        {
            var request = new GetCardsRequest() { CardIds = cardIds };
            var reply = await _client.GetCards(request, Timeouts.GLOBAL);

            if (!reply.Success) { return StatusCode(500); }
            return Ok(reply.Response);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveCards(Card[] cards)
        {
            var request = new SaveCardsRequest() { CardData = cards };
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
                    if (newCard.Name == "Shed Weakness")
                        newCard.ImageUris = item.ImageUris == null ? "" : item.ImageUris.ToString();
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
                    Console.Out.WriteLine("Something went wrong " + item);
                }

            }
            return target.ToArray();
        }
    }
}