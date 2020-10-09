using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderPayment
	{
		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		[Column(TypeName = "varchar(12)")]
		public string OrderNo { get; set; } = null!;

		[Column(TypeName = "varchar(64)")]
		public string? ExternalOrderNo { get; set; }

		[Column(TypeName = "varchar(64)")]
		public string? FromAccountId { get; set; }

		[Column(TypeName = "varchar(64)")]
		public string? FromAccountName { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		public PaymentChoise PaymentChoise { get; set; }

		public PaymentStatus PaymentStatus { get; set; }

		public DateTime? ConfirmedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		[JsonIgnore]
		public Order Order { get; set; } = null!;

		public List<OrderRefund> Refunds { get; set; } = new List<OrderRefund>();


		// calculation

		public bool IsTimeout => PaymentStatus == PaymentStatus.Await && CreatedTime < DateTime.Now.AddHours(-48);
	}
}
