using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Service
{
    public class ModelMapper
    {
        public static Client.Models.Card MapCardData(JObject source)
        {
            Client.Models.Card target = new Client.Models.Card();

            target.Id = source["id"].Value<string>();
            target.Name = source["name"].Value<string>();
            //target.ImageUris = source["image_uris"].Value<string[]>();
            target.ManaCost = source["mana_cost"].Value<string>();
            target.ConvertedManaCost = source["cmc"].Value<int>();
            //target.Type = source["type_line"].Value<string>();
            target.Text = source["oracle_text"].Value<string>();
            //target.Colours = source["colors"].Value<string[]>();
            target.Set = source["set"].Value<string>();
            target.SetName = source["set_name"].Value<string>();
            target.Rarity = source["rarity"].Value<string>();
            target.Value = source["usd"].Value<decimal>();

            return target;
        }
    }
}
