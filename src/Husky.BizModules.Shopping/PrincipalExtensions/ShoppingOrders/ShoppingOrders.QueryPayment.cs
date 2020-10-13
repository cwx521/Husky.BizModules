using System;
using System.Linq;
using System.Threading.Tasks;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;

namespace Husky.Principal
{
	public partial class UserShoppingOrdersManager
	{
		public async Task<Result> QueryPayment(int paymentId) {
			if ( _me.IsAnonymous ) {
				return new Failure("请先登录");
			}

			var payment = _db.OrderPayments
				.Where(x => x.Id == paymentId)
				.Where(x => x.Order.BuyerId == _me.Id)
				.SingleOrDefault();

			if ( payment == null ) {
				return new Failure("查询失败，未找到支付信息");
			}
			if ( payment.Status == PaymentStatus.Paid ) {
				return new Success("已成功付款");
			}

			switch ( payment.Choise ) {
				case PaymentChoise.Alipay:
					if ( _alipay == null ) {
						throw new ArgumentNullException(nameof(_alipay));
					}
					var alipayResult = _alipay.QueryOrder(payment.PaymentNo);
					if ( alipayResult.Ok && alipayResult.TotalAmount == payment.Amount ) {
						payment.Status = PaymentStatus.Paid;
						payment.ExternalUserName = alipayResult.AlipayBuyerUserId;
					}
					break;

				case PaymentChoise.WeChat:
					if ( _wechat == null ) {
						throw new ArgumentNullException(nameof(_wechat));
					}
					var wechatPayResult = _wechat.PayService().QueryOrder(payment.AppId!, payment.PaymentNo);
					if ( wechatPayResult.Ok && wechatPayResult.Amount == payment.Amount ) {
						payment.Status = PaymentStatus.Paid;
					}
					break;
			}

			await _db.Normalize().SaveChangesAsync();

			return payment.Status == PaymentStatus.Paid
				? new Result(true, "已成功付款")
				: new Result(false, "未找到付款信息");
		}
	}
}
