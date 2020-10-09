using Husky;

namespace Husky.BizModules.Shopping.DataModels
{
	public enum PaymentChoise
	{
		[Label("账户余额")]
		Credit,

		[Label("微信支付")]
		WeChat,

		[Label("支付宝")]
		Alipay
	}
}
