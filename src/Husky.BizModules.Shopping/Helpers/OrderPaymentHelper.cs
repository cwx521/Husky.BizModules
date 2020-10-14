using System;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;
using Husky.WeChatIntegration;
using Microsoft.AspNetCore.Http;

namespace Husky.BizModules.Shopping
{
	public static class OrderPaymentHelper
	{
		public static WeChatPayOrderModel GenerateWeChatPayOrderModel(
			this OrderPayment payment,
			HttpContext httpContext,
			string subject,
			string notifyUrl = "~/pay/notify",
			WeChatPayTradeType tradeType = WeChatPayTradeType.JsApi,
			bool allowCreditCard = true) {

			if ( payment.AppId == null ) {
				throw new ArgumentNullException($"{nameof(payment)}.{nameof(OrderPayment.AppId)}");
			}
			if ( payment.Attach == null ) {
				throw new ArgumentNullException($"{nameof(payment)}.{nameof(OrderPayment.Attach)}");
			}
			if ( payment.ExternalUserId == null ) {
				throw new ArgumentNullException($"{nameof(payment)}.{nameof(OrderPayment.ExternalUserId)}");
			}
			if ( payment.PaymentNo == null ) {
				throw new ArgumentNullException($"{nameof(payment)}.{nameof(OrderPayment.PaymentNo)}");
			}
			if ( payment.Amount <= 0 ) {
				throw new ArgumentException($"{nameof(payment)}.{nameof(OrderPayment.Amount)} must be greater than Zero.");
			}

			return new WeChatPayOrderModel {
				AppId = payment.AppId,
				Attach = payment.Attach,
				OpenId = payment.ExternalUserId,
				OrderNo = payment.PaymentNo,
				Amount = payment.Amount,
				Body = subject,
				IPAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
				NotifyUrl = httpContext.ResolveUrl(notifyUrl),
				TradeType = tradeType,
				AllowCreditCard = allowCreditCard
			};
		}

		public static AlipayOrderModel GenerateAlipayOrderModel(
			this OrderPayment payment,
			HttpContext httpContext,
			string subject,
			string notifyUrl = "~/pay/notify",
			string callbackUrl = "~/pay/callback") {

			if ( payment.PaymentNo == null ) {
				throw new ArgumentNullException($"{nameof(payment)}.{nameof(OrderPayment.PaymentNo)}");
			}
			if ( payment.Amount <= 0 ) {
				throw new ArgumentException($"{nameof(payment)}.{nameof(OrderPayment.Amount)} must be greater than Zero.");
			}

			return new AlipayOrderModel {
				OrderNo = payment.PaymentNo,
				Amount = payment.Amount,
				Subject = subject,
				CallbackUrl = httpContext.ResolveUrl(callbackUrl),
				NotifyUrl = httpContext.ResolveUrl(notifyUrl),
			};
		}

		private static string ResolveUrl(this HttpContext httpContext, string url) => url.StartsWith("~/") ? httpContext.Request.SchemeAndHost() + url.Substring(1) : url;
	}
}
