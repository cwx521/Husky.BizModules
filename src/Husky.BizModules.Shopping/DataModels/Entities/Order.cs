using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Husky.BizModules.Users.DataModels;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public int BuyerId { get; set; }

		[Column(TypeName = "varchar(12)")]
		public string OrderNo { get; set; } = null!;

		public OrderStatus Status { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ActualTotalAmount { get; set; }

		public DateTime? CompletedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public User Buyer { get; set; } = null!;
		public OrderReceiverAddress? ReceiverAddress { get; set; }
		public List<OrderItem> Items { get; set; } = new List<OrderItem>();
		public List<OrderLog> Logs { get; set; } = new List<OrderLog>();
		public List<OrderPayment> Payments { get; set; } = new List<OrderPayment>();


		// calculation

		public string StatusColorScheme {
			get {
				switch ( Status ) {
					default:
					case OrderStatus.Deleted:
					case OrderStatus.Cancelled:
						return "secondary";
					case OrderStatus.PaidPartial:
					case OrderStatus.Paid:
						return "warning";
					case OrderStatus.AwaitDelivery:
					case OrderStatus.Delivered:
						return "primary";
					case OrderStatus.AwaitPayment:
					case OrderStatus.AfterService:
						return "danger";
					case OrderStatus.Completed:
						return "success";
				}
			}
		}
	}
}
