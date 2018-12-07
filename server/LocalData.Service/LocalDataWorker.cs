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
        string scryfallDataLocation = "C:/Dev/CardVault/Data/scryfall-all-cards.json";
        string spreadsheetFileLocation = "C:/Dev/CardVault/Data/LeagueCards.xlsx";
        #endregion

        #region -  Constructor  -
        public LocalDataWorker(ConnectionFactory factory) : base(factory)
        {
			ReadAndReply<LoadCardDataRequest, (CollectionData[], LocalCardData[])>(Queueing.Queues.GetCollectionData, true, LoadDataFromSpreadsheet);
		}

		#endregion

		#region -  Event Handlers  -

		private Task<(CollectionData[], LocalCardData[])> LoadDataFromSpreadsheet(object sender, ReceiveEventArgs<LoadCardDataRequest> e)
		{
            var heldCards = GetLocalCardData(sender, e).Result;
			var scryfallData = GetScryfallData(scryfallDataLocation).Result;

			List<CollectionData> collectionData = new List<CollectionData>();
            List<LocalCardData> errorCards = new List<LocalCardData>();
            heldCards.ForEach(card =>
			{
				try
				{
                    var foundName = scryfallData.First(d => d.Name == card.Name);
                    if(foundName != null)
                    {
                        collectionData.Add(ModelMapper.MapCollectionData(foundName, card));
                    } else
                    {
                        errorCards.Add(card);
                    }
                    
				}
				catch
				{
                    errorCards.Add(card);
                }
			});

            var result = (collectionData.ToArray(), errorCards.ToArray());
			return Task.FromResult(result);
		}


		private async Task<List<LocalCardData>> GetLocalCardData(object sender, ReceiveEventArgs<LoadCardDataRequest> e)
        {
            List<LocalCardData> allCards = new List<LocalCardData>();
            allCards.AddRange(await MapSpreadsheetData(spreadsheetFileLocation));
            return allCards;
        }

        private async Task<List<LocalScryfallData>> GetScryfallData(string scryfallDataLocation)
        {
            List<LocalScryfallData> items = new List<LocalScryfallData>();
            
            using (StreamReader r = new StreamReader(scryfallDataLocation))
            {
				foreach (dynamic scryfallDetail in JsonConvert.DeserializeObject<dynamic[]>(r.ReadToEnd()))
					items.Add(await ModelMapper.MapJsonData(scryfallDetail));
            }

            return items;
        }


        private Task<List<LocalCardData>> MapSpreadsheetData(string fileLocation)
        {
            List<LocalCardData> spreadsheetCardList = new List<LocalCardData>();
            var package = new ExcelPackage(new FileInfo(fileLocation));
            var totalSheets = package.Workbook.Worksheets.Count;

            for(int i = 1; i <= totalSheets; i++)
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[i];
                for (int row = workSheet.Dimension.Start.Row + 1; row <= workSheet.Dimension.End.Row; row++)
                {
                    try
                    {
                        string spreadsheetCardName = workSheet.Cells[row, 2].Value.ToString();
                        if (spreadsheetCardName != null)
                        {
                            int cardQuantity = Int32.Parse(workSheet.Cells[row, 3].Value.ToString());
                            spreadsheetCardList.Add(new LocalCardData { Name = spreadsheetCardName, Quantity = cardQuantity });
                        }
                    } catch (Exception ex) {
                        Console.Out.WriteLine("Error in mapping from spreadsheet " + ex);
                    }
                    
                }
            }

            return Task.FromResult(spreadsheetCardList);
        }
        #endregion
    }
}
