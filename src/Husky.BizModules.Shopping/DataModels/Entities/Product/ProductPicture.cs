using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductPicture
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }

		[StringLength(50)]
		public string? PictureName { get; set; }

		[StringLength(500), Column(TypeName = "varchar(500)")]
		public string PictureUrl { get; set; } = null!;

		public bool UseInCarousel { get; set; }

		public MediaType Type { get; set; }

		public RowStatus Status { get; set; }


		// nav props

		public Product Product { get; set; } = null!;
	}
}
