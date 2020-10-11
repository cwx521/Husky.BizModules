﻿using System;
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
		public string BuyerName { get; set; } = null!;

		[Column(TypeName = "varchar(12)")]
		public string OrderNo { get; set; } = null!;

		public OrderStatus Status { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ActualTotalAmount { get; set; }

		public bool HasPayBalance { get; set; }

		public Rating? Rating { get; set; }

		public bool IsFinalized { get; set; }

		public DateTime? CompletedTime { get; set; }

		public DateTime? FinalizedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public OrderReceiverAddress? ReceiverAddress { get; set; }
		public List<OrderItem> Items { get; set; } = new List<OrderItem>();
		public List<OrderExpress> Expresses { get; set; } = new List<OrderExpress>();
		public List<OrderLog> Logs { get; set; } = new List<OrderLog>();
		public List<OrderPayment> Payments { get; set; } = new List<OrderPayment>();
		public List<OrderComment> Comments { get; set; } = new List<OrderComment>();


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
					case OrderStatus.Delivering:
					case OrderStatus.Delivered:
						return "primary";
					case OrderStatus.AwaitPay:
					case OrderStatus.ServiceCare:
						return "danger";
					case OrderStatus.Completed:
						return "success";
				}
			}
		}
	}
}
