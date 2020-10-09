namespace Husky.BizModules.Shopping.DataModels
{
	public enum OrderStatus
	{
		[Label("待付款")]
		AwaitPayment = 0,

		[Label("待付全款")]
		PaidPartial = 11,

		[Label("已付款")]
		Paid = 15,

		[Label("待发货")]
		AwaitDelivery = 100,

		[Label("已发货")]
		Delivered = 101,

		[Label("售后中")]
		AfterService = 200,

		[Label("已完成")]
		Completed = 300,

		[Label("已取消")]
		Cancelled = 400,

		[Label("已取消")]
		Rejected = 401,

		[Label("已删除")]
		Deleted = 500
	}

	public static class OrderStatusCollection
	{
		public static OrderStatus[] ActiveStatusArray => new[] {
			OrderStatus.PaidPartial,
			OrderStatus.Paid,
			OrderStatus.AwaitDelivery,
			OrderStatus.Delivered
		};

		public static OrderStatus[] AwaitPayStatusArray => new[] {
			OrderStatus.AwaitPayment,
			OrderStatus.PaidPartial
		};

		public static OrderStatus[] BuyerAwaitStatusArray => new[] {
			OrderStatus.Paid,
			OrderStatus.AwaitDelivery,
			OrderStatus.AfterService
		};

		public static OrderStatus[] FinishedStatusArray => new[] {
			OrderStatus.Cancelled,
			OrderStatus.Rejected,
			OrderStatus.Completed
		};

		public static OrderStatus[] CountableStatusArray => new[] {
			OrderStatus.PaidPartial,
			OrderStatus.Paid,
			OrderStatus.AwaitDelivery,
			OrderStatus.Delivered,
			OrderStatus.AfterService,
			OrderStatus.Completed
		};
	}
}

