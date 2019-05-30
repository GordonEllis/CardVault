using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.Client.Models
{
	[Table("IMAGE_URIS")]
	public class ImageUri
    {
		[Column("CardId")]
		public string Id { get; set; }
		[Column("Small")]
		public string Small { get; set; }
		[Column("Normal")]
		public string Normal { get; set; }
		[Column("Large")]
		public string Large { get; set; }
		[Column("Png")]
		public string Png { get; set; }
		[Column("Art_Crop")]
		public string Art_Crop { get; set; }
		[Column("Border_Crop")]
		public string Border_Crop { get; set; }


		[ForeignKey("Id")]
		public Card Card { get; set; }
	}
}
