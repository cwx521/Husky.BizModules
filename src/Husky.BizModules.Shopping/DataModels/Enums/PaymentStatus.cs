using Husky;

namespace Husky.BizModules.Shopping.DataModels
{
	public enum PaymentStatus
	{
		[Label("待付款")]
		Await,

		[Label("待确认")]
		Checking,

		[Label("已付款", CssClass = "text-success")]
		Paid
	}
}
