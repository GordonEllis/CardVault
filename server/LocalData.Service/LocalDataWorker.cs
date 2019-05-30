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
			//Interrogate the spreadsheet and return the name/quantity pairs there
			var heldCards = GetLocalCardData(sender, e).Result;
			//Get the latest scryfall info that we've downloaded
			var scryfallData = GetScryfallData(scryfallDataLocation).Result;

			List<CollectionData> collectionData = new List<CollectionData>();
            List<LocalCardData> errorCards = new List<LocalCardData>();
            heldCards.ForEach(card =>
			{
				try
				{
					//Try and find our held card in the scryfall data
                    var foundName = scryfallData.First(d => d.Name == card.Name && d.Set.ToUpper() == card.Set.ToUpper());
                    if(foundName != null)
                    {
						//if we have a match, extract the data
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

        private Task<List<LocalScryfallData>> GetScryfallData(string scryfallDataLocation)
        {
            List<LocalScryfallData> items = new List<LocalScryfallData>();
            
            using (StreamReader r = new StreamReader(scryfallDataLocation))
            {
				foreach (dynamic scryfallDetail in JsonConvert.DeserializeObject<dynamic[]>(r.ReadToEnd()))
                    try
                    {
                        items.Add(ModelMapper.MapJsonData(scryfallDetail));
                    } catch
                    {
                        items.Add(ModelMapper.MapJsonData(scryfallDetail));
                    }		
            }

            return Task.FromResult(items);
        }


        private Task<List<LocalCardData>> MapSpreadsheetData(string fileLocation)
        {
            FileInfo file = new FileInfo(fileLocation);
            List<LocalCardData> spreadsheetCardList = new List<LocalCardData>();
            var package = new ExcelPackage(file);
            var totalSheets = package.Workbook.Worksheets.Count;

			//For each sheet
            for(int i = 1; i <= totalSheets; i++)
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[i];
				//for each row in the sheet
                for (int row = workSheet.Dimension.Start.Row + 1; row <= workSheet.Dimension.End.Row; row++)
                {
                    try
                    {
						//as long as we have a card name, add the name and quantity to our list
                        if (workSheet.Cells[row, 2].Value.ToString() != null)
                        {
                            string spreadsheetCardName = workSheet.Cells[row, 2].Value.ToString();
                            int cardQuantity = Int32.Parse(workSheet.Cells[row, 3].Value.ToString());
							string set = workSheet.Cells[row, 1].Value.ToString();
							spreadsheetCardList.Add(new LocalCardData { Name = spreadsheetCardName, Quantity = cardQuantity, Set = set });
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
