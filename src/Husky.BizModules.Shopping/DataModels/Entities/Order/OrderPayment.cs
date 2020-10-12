using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderPayment
	{
		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		[MaxLength(12), Column(TypeName = "varchar(12)"), Index(IsUnique = true)]
		public string PaymentNo { get; set; } = null!;

		[MaxLength(64), Column(TypeName = "varchar(64)")]
		public string? ExternalTransactionId { get; set; }

		[MaxLength(64)]
		public string? ExternalUserId { get; set; }

		[MaxLength(64)]
		public string? ExternalUserName { get; set; }

		[MaxLength(64), Column(TypeName = "varchar(64)")]
		public string? AppId { get; set; }

		[MaxLength(50)]
		public string? Attach { get; set; } = Guid.NewGuid().ToString();

		public PaymentChoise Choise { get; set; }

		public PaymentStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime StatusChangedTime { get; set; } = DateTime.Now;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Order? Order { get; set; }
		public List<OrderRefund> Refunds { get; set; } = new List<OrderRefund>();
	}
}
