using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductVariation
	{
		[Key]
		public int Id { get; set; }

		public int GroupId { get; set; }

		[StringLength(12), Column(TypeName = "varchar(12)")]
		public string? SkuCode { get; set; }

		[StringLength(24), Required]
		public string VariationName { get; set; } = null!;

		[StringLength(500), Column(TypeName = "varchar(500)")]
		public string? VariationPictureUrl { get; set; }

		public RowStatus Status { get; set; }


		// nav props

		public ProductVariationGroup Group { get; set; } = null!;
	}
}