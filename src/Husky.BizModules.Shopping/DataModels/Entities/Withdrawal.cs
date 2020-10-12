﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Withdrawal
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		[MaxLength(50)]
		public string? UserName { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		[Column(TypeName = "varchar(64)"), Index(IsUnique = true)]
		public string? ExternalTransactionId { get; set; }

		public PaymentChoise TargetChoise { get; set; }

		[MaxLength(50)]
		public string TargetAccount { get; set; } = null!;

		[MaxLength(50)]
		public string TargetAccountAlias { get; set; } = null!;

		public WithdrawalStatus Status { get; set; }

		public int? ByAdminId { get; set; }

		[MaxLength(24)]
		public string? ByAdminName { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime StatusChangedTime { get; set; } = DateTime.Now;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public List<OrderFinalize> AssosicatedOrderFinalizes { get; set; } = new List<OrderFinalize>();
	}
}