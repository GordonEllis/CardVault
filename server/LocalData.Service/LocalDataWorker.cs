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

		private async Task<(CollectionData[], LocalCardData[])> LoadDataFromSpreadsheet(object sender, ReceiveEventArgs<LoadCardDataRequest> e)
		{
			List<CollectionData> collectionData = new List<CollectionData>();
			List<LocalCardData> errorCards = new List<LocalCardData>();
			List<LocalCardData> cardsInSpreadsheet = await MapSpreadsheetData(spreadsheetFileLocation);
			List<LocalScryfallData> scryfallData = GetScryfallData(scryfallDataLocation).Result;

			cardsInSpreadsheet.ForEach(card =>
			{
				//Try and find our held card in the scryfall data
				LocalScryfallData foundName = scryfallData.FirstOrDefault(d => d.Name == card.Name && d.Set.ToUpper() == card.Set.ToUpper());
                if(foundName != null)
                {
					collectionData.Add(ModelMapper.MapCollectionData(foundName, card)); //if we have a match, extract the data
				} else
                {
					errorCards.Add(card); //If we don't have a match, add the card to our investigation list
                }
			});

			return (collectionData.ToArray(), errorCards.ToArray());
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
            List<LocalCardData> spreadsheetCardList = new List<LocalCardData>(); //Empty list to hold spreadsheet data
			ExcelPackage package = new ExcelPackage(new FileInfo(fileLocation)); //Get the spreadshet info

            for(int i = 1; i <= package.Workbook.Worksheets.Count; i++)  //For each sheet in the workbook
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[i];
				
                for (int row = workSheet.Dimension.Start.Row + 1; row <= workSheet.Dimension.End.Row; row++) //For each row in the sheet
				{
                    try
                    {
						//as long as we have a card name, add the name, quantity and set to our list
                        if (workSheet.Cells[row, 2].Value.ToString() != null)
                        {
							string set = workSheet.Cells[row, 1].Value.ToString();
							string spreadsheetCardName = workSheet.Cells[row, 2].Value.ToString();
                            int cardQuantity = Int32.Parse(workSheet.Cells[row, 3].Value.ToString());
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
