using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public int BuyerId { get; set; }

		[MaxLength(36)]
		public string? BuyerName { get; set; }

		[MaxLength(12), Column(TypeName = "varchar(12)"), Index(IsUnique = true)]
		public string OrderNo { get; set; } = null!;

		public OrderStatus Status { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal TotalAmount { get; set; }

		public bool HasPayBalance { get; set; }

		public Rating? Rating { get; set; }

		public DateTime? CompletedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public OrderReceiverAddress? ReceiverAddress { get; set; }
		public OrderFinalize? Finalize { get; set; }
		public List<OrderItem> Items { get; set; } = new List<OrderItem>();
		public List<OrderExpress> Expresses { get; set; } = new List<OrderExpress>();
		public List<OrderLog> Logs { get; set; } = new List<OrderLog>();
		public List<OrderPayment> Payments { get; set; } = new List<OrderPayment>();
		public List<OrderComment> Comments { get; set; } = new List<OrderComment>();


		// calculation

		public bool IsTimeOut => Status == OrderStatus.AwaitPay && CreatedTime < DateTime.Now.AddHours(-2);

		public string StatusColorScheme {
			get {
				switch ( Status ) {
					default:
					case OrderStatus.Cancelled:
					case OrderStatus.Closed:
					case OrderStatus.Deleted:
						return "secondary";
					case OrderStatus.Paid:
					case OrderStatus.PaidPartial:
						return "warning";
					case OrderStatus.Delivering:
					case OrderStatus.Delivered:
						return "primary";
					case OrderStatus.AwaitPay:
					case OrderStatus.AwaitPayBalance:
					case OrderStatus.ServiceCare:
					case OrderStatus.Returning:
					case OrderStatus.Returned:
						return "danger";
					case OrderStatus.Received:
					case OrderStatus.Completed:
						return "success";
				}
			}
		}
	}
}
