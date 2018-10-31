using LocalData.Client;
using LocalData.Client.Models;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

using OfficeOpenXml;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace LocalData.Service
{
    class LocalDataWorker : QueueReaderService
    {
        #region - Fields - 
        #endregion

        #region -  Constructor  -
        public LocalDataWorker(ConnectionFactory factory) : base(factory)
        {
            ReadAndReply<LoadCardDataRequest, LocalCardData[]>(Queueing.Queues.GetLocalCardData, true, GetLocalCardData);
            ReadAndReply<LoadScryfallDataRequest, LocalScryfallData[]>(Queueing.Queues.GetLocalScryfallData, true, GetLocalScryfallData);
			ReadAndReply<LoadCardDataRequest, CollectionData[]>(Queueing.Queues.GetCollectionData, true, GetCollecionData);
		}

		#endregion

		#region -  Event Handlers  -

		private async Task<CollectionData[]> GetCollecionData(object sender, ReceiveEventArgs<LoadCardDataRequest> e)
		{
			var heldCards = GetLocalCardData(sender, e).Result.ToList();
			var scryfallData = GetJson().Result.ToList();

			List<CollectionData> collectionData = new List<CollectionData>();
			heldCards.ForEach(card =>
			{
				try
				{
					collectionData.Add(ModelMapper.MapCollectionData(scryfallData.First(d => d.Name == card.Name), card));
				}
				catch (Exception x)
				{}
			});

			var result = collectionData.ToArray();
			return result;
		}


		private async Task<LocalCardData[]> GetLocalCardData(object sender, ReceiveEventArgs<LoadCardDataRequest> e)
        {
            var allCards = new List<LocalCardData>();
            allCards.AddRange(await GetCards("Rare"));
            allCards.AddRange(await GetCards("Multi"));
            allCards.AddRange(await GetCards("Red"));
            allCards.AddRange(await GetCards("Blue"));
            allCards.AddRange(await GetCards("Black"));
            allCards.AddRange(await GetCards("Green"));
            allCards.AddRange(await GetCards("White"));
            var result = allCards.ToArray();
            return allCards.ToArray();
        }

        private async Task<LocalScryfallData[]> GetLocalScryfallData(object sender, ReceiveEventArgs<LoadScryfallDataRequest> e)
        {
            var jsonList = await GetJson();
			var returnlist = jsonList.Where(i => e.Message.CardNames.Contains(i.Name)).ToArray();
			return returnlist;

		}

        private async Task<LocalScryfallData[]> GetJson()
        {
            List<LocalScryfallData> items = new List<LocalScryfallData>();
            
            using (StreamReader r = new StreamReader("C:/Dev/CardVault/Data/scryfall-all-cards.json"))
            {
                string json = r.ReadToEnd();
				var jello = JsonConvert.DeserializeObject<dynamic[]>(json);
				foreach (dynamic cardDetail in jello)
					items.Add(ModelMapper.MapJsonData(cardDetail));
            }
            return items.ToArray();
        }


        private async Task<LocalCardData[]> GetCards(string type)
        {
            List<LocalCardData> cardList = new List<LocalCardData>();
            var fileInfo = new FileInfo("C:/Dev/CardVault/Data/league" + type + ".xlsx");
            var package = new ExcelPackage(fileInfo);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];


            for (int i = workSheet.Dimension.Start.Row;
                i <= workSheet.Dimension.End.Row;
                i++){
                string cardName = workSheet.Cells[i, 1].Value.ToString();
                int cardQuantity = Int32.Parse(workSheet.Cells[i, 2].Value.ToString());
                cardList.Add(new LocalCardData { Name = cardName, Quantity = cardQuantity });
                }
            return cardList.ToArray();
        }
        #endregion
    }
}
