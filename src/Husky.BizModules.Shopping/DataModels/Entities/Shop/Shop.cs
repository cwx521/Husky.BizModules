using System;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Shop
	{
		public int Id { get; set; }

		public int OwnerId { get; set; }

		[StringLength(36)]
		public string? OwnerName { get; set; }

		[StringLength(50), Required]
		public string ShopName { get; set; } = null!;

		[StringLength(500)]
		public string? Description { get; set; }

		public RowStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public ShopLimit Limit { get; set; } = null!;
	}
}
