using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductVariation
	{
		[Key]
		public int Id { get; set; }

		public int GroupId { get; set; }

		[MaxLength(12), Column(TypeName = "varchar(12)")]
		public string? SkuCode { get; set; }

		[MaxLength(24)]
		public string VariationName { get; set; } = null!;

		[MaxLength(500), Column(TypeName = "varchar(500)")]
		public string? VariationPictureUrl { get; set; }

		public RowStatus Status { get; set; }


		// nav props

		public ProductVariationGroup Group { get; set; } = null!;
	}
}