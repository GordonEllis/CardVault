using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Decks.Client.Models
{
	[Table("DECKS")]
	public class Deck
	{
		[Key]
		[Column("DeckId")]
		public int DeckId { get; set; }
		[Column("DeckName")]
		public string Name { get; set; }
		[Column("DeckDescription")]
		public string Description { get; set; }


		public List<DeckCard> DeckCards { get; set; }
	}
}
