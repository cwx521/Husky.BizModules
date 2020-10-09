using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;
using Husky.WeChatIntegration;
using Microsoft.AspNetCore.Http;

namespace Husky.BizModules.Shopping
{
	public static class OrderPaymentHelper
	{
		public static WeChatPayOrderModel GenerateWeChatPayOrderModel(
			this Order order,
			HttpContext httpContext,
			string wechatAppId,
			string wechatUserOpenId,
			string subject,
			string notifyUrl,
			bool allowCreditCard = true,
			WeChatPayTradeType tradeType = WeChatPayTradeType.JsApi) {

			return new WeChatPayOrderModel {
				AppId = wechatAppId,
				OpenId = wechatUserOpenId,
				OrderId = order.OrderNo,
				Amount = order.ActualTotalAmount,
				Body = subject,
				IPAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
				NotifyUrl = notifyUrl,
				TradeType = tradeType,
				AllowCreditCard = allowCreditCard
			};
		}

		public static AlipayPayment GenerateAlipayOrderModel(this Order order, string subject, string callbackUrl, string notifyUrl) {
			return new AlipayPayment {
				OrderId = order.OrderNo,
				Amount = order.ActualTotalAmount,
				Subject = subject,
				CallbackUrl = callbackUrl,
				NotifyUrl = notifyUrl
			};
		}
	}
}
