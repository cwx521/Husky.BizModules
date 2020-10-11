using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderRefund
	{
		[Key]
		public int Id { get; set; }

		public int SourcePaymentId { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		[Column(TypeName = "varchar(12)")]
		public string RefundNo { get; set; } = null!;

		public RefundReason Reason { get; set; }

		public RefundStatus Status { get; set; }

		[MaxLength(100)]
		public string? ResultMessage { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime StatusChangedTime { get; set; } = DateTime.Now;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		[JsonIgnore]
		public OrderPayment SourcePayment { get; set; } = null!;


		// calculation

		public bool IsTimeout => Status == RefundStatus.Await && CreatedTime < DateTime.Now.AddHours(-48);
	}
}
