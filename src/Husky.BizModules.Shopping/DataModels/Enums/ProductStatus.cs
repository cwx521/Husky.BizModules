using Husky;

namespace Husky.BizModules.Shopping.DataModels
{
	public enum ProductStatus
	{
		[Label("正常", CssClass = "text-success")]
		Active,

		[Label("待审核", CssClass = "text-muted")]
		AwaitApproval,

		[Label("无货", CssClass = "text-muted")]
		OutOfStock,

		[Label("暂停供应", CssClass = "text-muted")]
		Onhold,

		[Label("下架", CssClass = "text-danger")]
		OffShelve
	}
}
