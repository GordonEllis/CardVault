using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.Client.Models
{
    [Table("CARD_DETAILS")]
    public class Card
    {
        [Key]
        [Required]
        [Column("CardId")]
        public string Id { get; set; }
        [Column("CardName")]
        public string Name { get; set; }
        [Column("ImageURIs")]
        public string ImageURIs { get; set; }
        [Column("ManaCost")]
        public string ManaCost{ get; set; }
        [Column("ConvertedManaCost")]
        public int ConvertedManaCost { get; set; }
        //[Column("Type")]
        //public string Type { get; set; }
        [Column("Text")]
        public string Text { get; set; }
        [Column("Colours")]
        public string Colours { get; set; }
        [Column("Set")]
        public string Set { get; set; }
        [Column("SetName")]
        public string SetName { get; set; }
        [Column("Rarity")]
        public string Rarity { get; set; }
        [Column("Value")]
        public decimal? Value { get; set; }
    }
}
