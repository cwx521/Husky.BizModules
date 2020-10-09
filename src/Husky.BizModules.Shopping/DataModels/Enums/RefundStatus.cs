using Husky;

namespace Husky.BizModules.Shopping.DataModels
{
	public enum RefundStatus
	{
		[Label("待退款")]
		Await,

		[Label("待确认")]
		Checking,

		[Label("已退款", CssClass = "text-success")]
		Refunded
	}
}
