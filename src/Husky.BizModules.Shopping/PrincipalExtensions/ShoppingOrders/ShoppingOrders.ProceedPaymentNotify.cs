using System;
using System.Linq;
using System.Threading.Tasks;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;

namespace Husky.Principal
{
	partial class UserShoppingOrdersManager
	{
		public async Task<string> ProceedPaymentNotify() {
			var told = new OrderPayment();
			var appointedSuccessResultPlainText = "success";

			//try Alipay
			if ( _alipay != null ) {
				var alipayResult = _alipay.ParseNotifyResult(_http.Request);
				if ( alipayResult.Ok && alipayResult.OrderId != null ) {

					told.Choise = PaymentChoise.Alipay;
					told.Amount = alipayResult.Amount;
					told.PaymentNo = alipayResult.OrderId;
					told.ExternalTransactionId = alipayResult.AlipayTradeNo;
					told.ExternalUserId = alipayResult.AlipayBuyerId;
				}
			}

			//try WeChat
			if ( _wechat != null ) {
				var wechatPay = _wechat.PayService();
				var wechatResult = wechatPay.ParseNotifyResult(_http.Request.Body);
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
				var payment = _db.OrderPayments
					.Where(x => x.Choise == told.Choise)
					.Where(x => x.PaymentNo == told.PaymentNo)
					.Where(x => x.Amount == told.Amount)
					.Where(x => x.Attach == told.Attach || told.Choise != PaymentChoise.WeChat)
					.FirstOrDefault();

				if ( payment != null ) {
					payment.Status = PaymentStatus.Paid;
					payment.StatusChangedTime = DateTime.Now;

					_db.OrderLogs.Add(new OrderLog {
						OrderId = payment.OrderId,
						Remarks = $"订单付款到账 {payment.Amount:f2} 元",
						IsOpen = true,
					});

					await _db.Normalize().SaveChangesAsync();
					return appointedSuccessResultPlainText;
				}
			}

			return "fail";
		}
	}
}
