namespace Husky.BizModules.Shopping.DataModels
{
	public enum Marking
	{

		[Label("极好", CssClass = "text-success")]
		Excellent,

		[Label("很好", CssClass = "text-success")]
		Good,

		[Label("一般", CssClass = "text-primary")]
		Ok,

		[Label("不满意", CssClass = "text-secondary")]
		Unsatisfied,

		[Label("非常差", CssClass = "text-danger")]
		Poor
	}
}
