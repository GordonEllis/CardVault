using Decks.Client;
using Decks.Client.Models;
using Decks.Service.Context;
using Newtonsoft.Json;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decks.Service
{
    class DecksWorker : QueueReaderService
    {
        #region - Fields - 

        private readonly DeckContext _context = new DeckContext();

        #endregion

        #region -  Constructor  -
		
        public DecksWorker(ConnectionFactory factory) : base(factory)
        {
			//ReadAndReply<DeleteDeckRequest, Boolean>(Queueing.Queues.DeleteDecks, true, DeleteDeck);
			ReadAndReply<SaveDeckRequest, Deck>(Queueing.Queues.SaveDeck, true, SaveDeck);
			ReadAndReply<GetDecksRequest, Deck[]>(Queueing.Queues.GetDecks, true, GetDecks);
			
        }

		#endregion

		#region -  Event Handlers  -
		private Task<Boolean> DeleteDeck(object sender, ReceiveEventArgs<DeleteDeckRequest> e)
		{
			e.Acknowledge = true;
			return Task.FromResult(true);
		}

		private async Task<Deck> SaveDeck(object sender, ReceiveEventArgs<SaveDeckRequest> e)
        {
			_context.ChangeTracker.AutoDetectChangesEnabled = false;

			var savedDeck = SaveDecktoDatabase(_context, e.Message.DeckData);
			var deckId = savedDeck.DeckId;
			SaveDeckCards(_context, deckId, savedDeck.DeckCards, e.Message.DeckData.DeckCards);

			_context.SaveChanges();

			e.Acknowledge = true;
			return await Task.FromResult(savedDeck);
		}

		private static Deck SaveDecktoDatabase(DeckContext context, Deck updatedDeck)
		{
			var savedDetails = updatedDeck;

			if (updatedDeck.DeckId == 0)
			#region Insert new deck
			{
				var newDeck = JsonConvert.DeserializeObject<Deck>(JsonConvert.SerializeObject(savedDetails));
				newDeck.DeckId = 0;
				newDeck.DeckCards.Clear();
				
				context.Add(newDeck);
				savedDetails = newDeck;
			}
			#endregion
			else
			#region Update Deck
			{
				savedDetails = context.Decks.First(g => g.DeckId == updatedDeck.DeckId);
			}
			#endregion

			savedDetails.Description = updatedDeck.Description;
			savedDetails.Name = updatedDeck.Name;

			return savedDetails;
		}

		public static void SaveDeckCards(DeckContext context, int deckId, List<DeckCard> existing, List<DeckCard> updated)
		{
			updated.ForEach(c => c.DeckId = deckId);

			var itemsToAdd = updated.Where(d => !existing.Any(c => c.DeckId == d.DeckId));
			var itemsToUpdate = existing.Where(d => updated.Any(c => c.DeckId == d.DeckId));
			var itemsToRemove = existing.Where(d => !updated.Any(c => c.DeckId == d.DeckId));

			foreach (var update in itemsToUpdate)
				context.Entry(update).CurrentValues.SetValues(updated.Where(d => d.DeckId == update.DeckId).SingleOrDefault());
			context.DeckCard.RemoveRange(itemsToRemove);
			context.DeckCard.AddRange(itemsToAdd);
		}

		private async Task<Deck[]> GetDecks(object sender, ReceiveEventArgs<GetDecksRequest> e)
        {
			return await Task.FromResult(_context.Decks.ToArray());
        }
    }
    #endregion
}