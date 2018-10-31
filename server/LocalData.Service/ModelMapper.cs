using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalData.Service
{
    public class ModelMapper
    {
        public static Client.Models.LocalScryfallData MapJsonData(JObject source)
        {
            Client.Models.LocalScryfallData target = new Client.Models.LocalScryfallData();

            target.Id = source["id"].Value<string>();
            target.Name = source["name"].Value<string>();
			try { target.ImageUris = source["image_uris"].Value<Object>(); } catch { target.ImageUris = null; };
			try { target.ManaCost = source["mana_cost"].Value<string>(); } catch { target.ManaCost = " "; };
			try { target.Cmc = source["cmc"].Value<int>(); } catch { target.Cmc = 0; };
			try { target.TypeLine = source["type_line"].Value<string>(); } catch { target.TypeLine = " "; };
			try { target.OracleText = source["oracle_text"].Value<string>(); } catch { target.OracleText = " "; };
			try { target.Colors = source["colors"].Value<Object>(); } catch { target.Colors = null; };
			try { target.Set = source["set"].Value<string>(); } catch { target.Set = " "; };
			try { target.SetName = source["set_name"].Value<string>(); } catch { target.SetName = " "; };
			try { target.Rarity = source["rarity"].Value<string>(); } catch { target.Rarity = " "; };

			return target;
        }

		public static Client.Models.CollectionData MapCollectionData(Client.Models.LocalScryfallData scryfallData, Client.Models.LocalCardData localData)
		{
			Client.Models.CollectionData target = new Client.Models.CollectionData();

			target.Id = scryfallData.Id;
			target.Name = scryfallData.Name;
			target.Quantity = localData.Quantity;
			target.ImageUris = scryfallData.ImageUris;
			target.ManaCost = scryfallData.ManaCost;
			target.Cmc = scryfallData.Cmc;
			target.TypeLine = scryfallData.TypeLine;
			target.OracleText = scryfallData.OracleText;
			target.Colors = scryfallData.Colors;
			target.Set = scryfallData.Set;
			target.SetName = scryfallData.SetName;
			target.Rarity = scryfallData.Rarity;

			return target;
		}
	}
}
