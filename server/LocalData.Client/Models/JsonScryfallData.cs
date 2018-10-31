namespace LocalData.Client.Models
{
    public class JsonScryfallData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string ImageUris { get; set; }
        public string ManaCost { get; set; }
        public int Cmc { get; set; }
        public string TypeLine { get; set; }
        public string OracleText { get; set; }
        public string Loyalty { get; set; }
        public string[] Colors { get; set; }
        public string[] ColorIdentity { get; set; }
        public string Set { get; set; }
        public string SetName { get; set; }
        public string Rarity { get; set; }
    }
}
