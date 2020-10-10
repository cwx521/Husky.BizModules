using System;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ShoppingCartItem
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }

		public int BuyerId { get; set; }

		[MaxLength(36)]
		public string BuyerName { get; set; } = null!;

		[MaxLength(100)]
		public string? VariationExpression { get; set; }

		[MaxLength(500)]
		public string? VariationDescription { get; set; }

		public int Quantity { get; set; }

		[MaxLength(200)]
		public string? Remarks { get; set; }

		public bool Selected { get; set; }

		public bool Removed { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Product Product { get; set; } = null!;
	}
}
