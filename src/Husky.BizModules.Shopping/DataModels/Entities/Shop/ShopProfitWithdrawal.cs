using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ShopProfitWithdrawal
	{
		[Key]
		public int Id { get; set; }

		public int ShopId { get; set; }

		[Column(TypeName = "decimal(9,2)")]
		public decimal Amount { get; set; }

		[MaxLength(64), Column(TypeName = "varchar(64)"), Index(IsUnique = true)]
		public string? ExternalTransactionId { get; set; }

		public PaymentChoise TargetChoise { get; set; }

		[MaxLength(50)]
		public string TargetAccount { get; set; } = null!;

		[MaxLength(50)]
		public string? TargetAccountAlias { get; set; }

		public int? OperatedByAdminId { get; set; }

		[MaxLength(24)]
		public string? OperatedByAdminName { get; set; }

		public WithdrawalStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime StatusChangedTime { get; set; } = DateTime.Now;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Shop? Shop { get; set; }
		public List<OrderFinalize> AssosicatedOrderFinalizes { get; set; } = new List<OrderFinalize>();
	}
}
