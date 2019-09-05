using System.ComponentModel.DataAnnotations.Schema;

namespace Decks.Client.Models
{
	[Table("DECK_CARDS")]
	public class DeckCard
	{
		[Column("DeckId")]
		public int DeckId { get; set; }

		[Column("CardId")]
		public string CardId { get; set; }

		[Column("Quantity")]
		public int Quantity { get; set; }
		
		[ForeignKey("DeckId")]
		public Deck Deck { get; set; }
	}
}
