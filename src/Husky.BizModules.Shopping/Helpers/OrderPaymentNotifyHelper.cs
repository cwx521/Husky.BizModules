using System;
using System.Linq;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;
using Husky.WeChatIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.BizModules.Shopping
{
	public static class OrderPaymentNotifyHelper
	{
		public static async Task<string> ProceedOrderPaymentNotify(this HttpContext httpContext) {
			var alipay = httpContext.RequestServices.GetService<AlipayService>();
			var wechat = httpContext.RequestServices.GetService<WeChatService>();
			var db = httpContext.RequestServices.GetRequiredService<IShoppingDbContext>();

			var told = new OrderPayment();
			var appointedSuccessResultPlainText = "success";

			//try Alipay
			if ( alipay != null ) {
				var alipayResult = alipay.ParseNotifyResult(httpContext.Request);
				if ( alipayResult.Ok && alipayResult.OrderId != null ) {

					told.Choise = PaymentChoise.Alipay;
					told.Amount = alipayResult.Amount;
					told.PaymentNo = alipayResult.OrderId;
					told.ExternalTransactionId = alipayResult.AlipayTradeNo;
					told.ExternalUserId = alipayResult.AlipayBuyerId;
				}
			}

			//try WeChat
			if ( wechat != null ) {
				var wechatPay = wechat.PayService();
				var wechatResult = wechatPay.ParseNotifyResult(httpContext.Request.Body);
				if ( wechatResult.Ok && wechatResult.OrderId != null ) {

					told.Choise = PaymentChoise.WeChat;
					told.Amount = wechatResult.Amount;
					told.PaymentNo = wechatResult.OrderId;
					told.ExternalTransactionId = wechatResult.TransactionId;
					told.ExternalUserId = wechatResult.OpenId;
					told.Attach = wechatResult.Attach;

					appointedSuccessResultPlainText = wechatPay.CreateNotifyRespondSuccessXml();
				}
			}

			//Unified to (OrderPayment)told
			//Then use this to look through the database

			if ( told.Amount != 0 && !string.IsNullOrEmpty(told.PaymentNo) ) {
				var payment = db.OrderPayments
					.Where(x => x.Choise == told.Choise)
					.Where(x => x.PaymentNo == told.PaymentNo)
					.Where(x => x.Amount == told.Amount)
					.Where(x => x.Attach == told.Attach || told.Choise != PaymentChoise.WeChat)
					.FirstOrDefault();

				if ( payment != null ) {
					payment.Status = PaymentStatus.Paid;
					payment.StatusChangedTime = DateTime.Now;

					db.OrderLogs.Add(new OrderLog {
						OrderId = payment.OrderId,
						Remarks = $"订单付款到账 {payment.Amount:f2} 元",
						IsOpen = true,
					});

					await db.Normalize().SaveChangesAsync();
					return appointedSuccessResultPlainText;
				}
			}

			return "fail";
		}
	}
}
