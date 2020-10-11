namespace Husky.BizModules.Shopping.DataModels
{
	public enum WithdrawalStatus
	{
		[Label("待处理")]
		New = 0,

		[Label("已取消", CssClass = "text-secondary")]
		Cancelled = 1,

		[Label("已审核", CssClass = "text-primary")]
		Accepted = 2,

		[Label("已拒绝", CssClass = "text-danger")]
		Rejected = 3,

		[Label("已出纳", CssClass = "text-success")]
		PaidOut = 9
	}
}
