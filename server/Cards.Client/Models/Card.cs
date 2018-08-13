using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.Models
{
    public class Card
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageURIs { get; set; }
        public string ManaCost{ get; set; }
        public int ConvertedManaCost { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Colours { get; set; }
        public string Set { get; set; }
        public string SetName { get; set; }
        public string Rarity { get; set; }
        public decimal? Value { get; set; }
    }
}
