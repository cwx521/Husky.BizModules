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

		[Column(TypeName = "varchar(12)")]
		public string RequestNo { get; set; } = null!;

		[Column(TypeName = "varchar(64)")]
		public string? ExternalOrderNo { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Amount { get; set; }

		public RefundStatus RefundStatus { get; set; }

		public DateTime? ConfirmedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		[JsonIgnore]
		public OrderPayment SourcePayment { get; set; } = null!;


		// calculation

		public bool IsTimeout => RefundStatus == RefundStatus.Await && CreatedTime < DateTime.Now.AddHours(-48);
	}
}
