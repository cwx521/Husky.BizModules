using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Shop
	{
		public int Id { get; set; }

		public int OwnerId { get; set; }

		[MaxLength(36)]
		public string? OwnerName { get; set; }

		[MaxLength(50)]
		public string ShopName { get; set; } = null!;

		[MaxLength(500)]
		public string? ShopDescription { get; set; }

		public RowStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public ShopLimit? Limit { get; set; }
		public List<Product> Products { get; set; } = new List<Product>();
		public List<ShopProfitWithdrawal> Withdrawals { get; set; } = new List<ShopProfitWithdrawal>();
	}
}
