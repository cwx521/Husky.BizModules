namespace Husky.BizModules.Shopping.DataModels
{
	public enum OrderStatus
	{
		[Label("待付款")]
		AwaitPay = 0,

		[Label("待付全款")]
		PaidPartial = 11,

		[Label("已付款")]
		Paid = 15,

		[Label("待发货")]
		Delivering = 100,

		[Label("待验收")]
		Delivered = 101,

		[Label("已验收")]
		Received = 105,

		[Label("售后中")]
		ServiceCare = 200,

		[Label("退货中")]
		Returning = 201,

		[Label("已退货")]
		Returned = 205,

		[Label("待付尾款")]
		AwaitPayBalance = 301,

		[Label("已付全款")]
		PaidBalance = 302,

		[Label("已完成")]
		Completed = 305,

		[Label("已取消")]
		Cancelled = 400,

		[Label("已关闭")]
		Closed = 401,

		[Label("已删除")]
		Deleted = 500
	}

	public static class OrderStatusArray
	{
		public static OrderStatus[] ResultfulStatusArray => new[] {
			OrderStatus.PaidPartial,
			OrderStatus.Paid,
			OrderStatus.Delivering,
			OrderStatus.Delivered,
			OrderStatus.Received,
			OrderStatus.ServiceCare,
			OrderStatus.Returning,
			OrderStatus.Returned,
			OrderStatus.AwaitPayBalance,
			OrderStatus.PaidBalance,
			OrderStatus.Completed
		};

		public static OrderStatus[] SellerCareStatusArray => new[] {
			OrderStatus.PaidPartial,
			OrderStatus.Paid,
			OrderStatus.Delivering,
			OrderStatus.Delivered,
			OrderStatus.Received,
			OrderStatus.ServiceCare,
			OrderStatus.Returning,
			OrderStatus.Returned,
			OrderStatus.AwaitPayBalance,
			OrderStatus.PaidBalance
		};

		public static OrderStatus[] AwaitPayStatusArray => new[] {
			OrderStatus.AwaitPay,
			OrderStatus.PaidPartial,
			OrderStatus.AwaitPayBalance
		};

		public static OrderStatus[] AwaitDeliverStatusArray => new[] {
			OrderStatus.Paid,
			OrderStatus.Delivering
		};

		public static OrderStatus[] AwaitReceiveStatusArray => new[] {
			OrderStatus.Delivered,
			OrderStatus.AwaitPayBalance
		};

		public static OrderStatus[] AwaitFinalizeStatusArray => new[] {
			OrderStatus.Received,
			OrderStatus.PaidBalance
		};

		public static OrderStatus[] ServiceCareStatusArray => new[] {
			OrderStatus.Returning,
			OrderStatus.Returned,
			OrderStatus.ServiceCare
		};
	}
}

