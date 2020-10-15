using System;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderCartItem
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }

		public int BuyerId { get; set; }

		[StringLength(36)]
		public string? BuyerName { get; set; }

		[StringLength(1000)]
		public string? VariationJson { get; set; }

		public int Quantity { get; set; }

		[StringLength(200)]
		public string? Remarks { get; set; }

		public bool Selected { get; set; }

		public bool Removed { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Product Product { get; set; } = null!;
	}
}
