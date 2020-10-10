using System;
using System.Linq;
using System.Threading.Tasks;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	partial class UserShoppingOrdersManager
	{
		public async Task<Result> QueryRefund(int refundId) {
			var refund = _db.OrderRefunds
				.Include(x => x.SourcePayment)
				.Where(x => x.Id == refundId)
				.Where(x => x.SourcePayment.Order.BuyerId == _me.Id)
				.SingleOrDefault();

			if ( refund == null ) {
				return new Failure("查询失败，未找到退款信息");
			}
			if ( refund.Status == RefundStatus.Refunded ) {
				return new Success("已成功退款");
			}

			Result result = null!;
			switch ( refund.SourcePayment.Choise ) {
				case PaymentChoise.Alipay:
					_ = _alipay ?? throw new ArgumentNullException(nameof(_alipay));
					result = _alipay.QueryRefund(refund.SourcePayment.PaymentNo, refund.RefundNo);
					break;

				case PaymentChoise.WeChat:
					_ = _wechat ?? throw new ArgumentNullException(nameof(_wechat));
					result = _wechat.PayService().QueryRefund(refund.SourcePayment.AppId!, refund.RefundNo);
					break;
			}

			await _db.Normalize().SaveChangesAsync();

			return refund.Status == RefundStatus.Refunded
				? new Result(true, "已成功付款")
				: new Result(false, result.Message ?? "未查询到退款信息");
		}
	}
}
