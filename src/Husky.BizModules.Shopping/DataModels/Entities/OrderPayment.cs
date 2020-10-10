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

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		[Column(TypeName = "varchar(12)")]
		public string PaymentNo { get; set; } = null!;

		[Column(TypeName = "varchar(64)")]
		public string? ExternalOrderNo { get; set; }

		[Column(TypeName = "varchar(64)")]
		public string? ExternalUserId { get; set; }

		[Column(TypeName = "varchar(64)")]
		public string? ExternalUserName { get; set; }

		[Column(TypeName = "varchar(64)")]
		public string? AppId { get; set; }

		[MaxLength(50)]
		public string? Attach { get; set; } = Guid.NewGuid().ToString();

		public PaymentChoise Choise { get; set; }

		public PaymentStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime StatusUpdatedTime { get; set; } = DateTime.Now;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		[JsonIgnore]
		public Order Order { get; set; } = null!;

		public List<OrderRefund> Refunds { get; set; } = new List<OrderRefund>();


		// calculation

		public bool IsTimeout => Status == PaymentStatus.Await && CreatedTime < DateTime.Now.AddHours(-48);
	}
}
