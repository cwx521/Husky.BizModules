using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderRefund
	{
		[Key]
		public int Id { get; set; }

		public int OriginalPaymentId { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		[StringLength(12), Column(TypeName = "varchar(12)"), Required, Unique]
		public string RefundNo { get; set; } = null!;

		[StringLength(100)]
		public string? ResultMessage { get; set; }

		public RefundReason Reason { get; set; }

		public RefundStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime StatusChangedTime { get; set; } = DateTime.Now;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public OrderPayment OriginalPayment { get; set; } = null!;


		// calculation

		public bool IsTimeout => Status == RefundStatus.Await && CreatedTime < DateTime.Now.AddHours(-48);
	}
}
