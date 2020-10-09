using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductPicture
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }

		[MaxLength(50)]
		public string? PictureName { get; set; }

		[MaxLength(500)]
		public string PictureUrl { get; set; } = null!;

		public bool UseInCarousel { get; set; }

		public MediaType Type { get; set; }

		public RowStatus Status { get; set; }


		// nav props

		[JsonIgnore]
		public Product Product { get; set; } = null!;
	}
}
