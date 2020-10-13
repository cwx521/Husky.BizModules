using System;
using System.Linq;
using System.Threading.Tasks;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserShoppingOrdersManager
	{
		public async Task<Result> QueryRefund(int refundId) {
			if ( _me.IsAnonymous ) {
				return new Failure("请先登录");
			}

			var refund = _db.OrderRefunds
				.Include(x => x.OriginalPayment)
				.Where(x => x.Id == refundId)
				.Where(x => x.OriginalPayment.Order.BuyerId == _me.Id)
				.SingleOrDefault();

			if ( refund == null ) {
				return new Failure("查询失败，未找到退款信息");
			}
			if ( refund.Status == RefundStatus.Refunded ) {
				return new Success("已成功退款");
			}

			Result result = null!;
			switch ( refund.OriginalPayment.Choise ) {
				case PaymentChoise.Alipay:
					_ = _alipay ?? throw new ArgumentNullException(nameof(_alipay));
					result = _alipay.QueryRefund(refund.OriginalPayment.PaymentNo, refund.RefundNo);
					break;

				case PaymentChoise.WeChat:
					_ = _wechat ?? throw new ArgumentNullException(nameof(_wechat));
					result = _wechat.PayService().QueryRefund(refund.OriginalPayment.AppId!, refund.RefundNo);
					break;
			}

			await _db.Normalize().SaveChangesAsync();

			return refund.Status == RefundStatus.Refunded
				? new Result(true, "已成功付款")
				: new Result(false, result.Message ?? "未查询到退款信息");
		}
	}
}
