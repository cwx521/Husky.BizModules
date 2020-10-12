using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Husky.Alipay;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserShoppingOrdersManager
	{
		public async Task<Result> ProceedRefund(int orderId, RefundReason refundReason, OrderStatus? setOrderStatus = null, decimal? requestRefundAmount = null) {
			const int daysToRefundAfterOrderCompleted = 30;

			var order = _db.Orders
				.Include(x => x.Payments).ThenInclude(x => x.Refunds)
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == orderId)
				.SingleOrDefault();

			if ( order == null ) {
				return new Failure("退款失败，未找到订单");
			}
			if ( order.CompletedTime.HasValue && order.CompletedTime < DateTime.Now.AddDays(daysToRefundAfterOrderCompleted) ) {
				return new Failure($"只能在订单完成后的 {daysToRefundAfterOrderCompleted} 天内进行退款");
			}

			if ( _alipay == null && order.Payments.Any(x => x.Choise == PaymentChoise.Alipay) ) {
				throw new ArgumentNullException(nameof(_alipay));
			}
			if ( _wechat == null && order.Payments.Any(x => x.Choise == PaymentChoise.WeChat) ) {
				throw new ArgumentNullException(nameof(_wechat));
			}

			var totalPaid = order.Payments
				.Where(x => x.Status == PaymentStatus.Paid)
				.Sum(x => x.Amount);

			var refundedBefore = order.Payments.SelectMany(x => x.Refunds)
				.Where(x => x.Status != RefundStatus.Cancelled)
				.Sum(x => x.Amount);

			var refundMaxAllowed = totalPaid - refundedBefore;

			if ( requestRefundAmount.HasValue && requestRefundAmount.Value > refundMaxAllowed ) {
				return new Failure("超出总共可退款金额");
			}

			var refundRemained = requestRefundAmount ?? refundMaxAllowed;
			var pending = new List<OrderRefund>();

			//遍历该订单下的付款记录，依次轮询并使用可退款金额，并插入待退款记录(OrderRefund)
			foreach ( var payment in order.Payments ) {
				if ( payment.Status != PaymentStatus.Paid ) {
					continue;
				}

				var refundable = payment.Amount - payment.Refunds.Where(x => x.Status != RefundStatus.Cancelled).Sum(x => x.Amount);
				var refunding = Math.Min(refundRemained, refundable);

				var refund = new OrderRefund {
					RefundNo = OrderIdGen.New(),
					Amount = refunding,
					Reason = refundReason,
					Status = RefundStatus.Await
				};

				payment.Refunds.Add(refund);
				pending.Add(refund);

				refundRemained -= refunding;

				if ( refundRemained <= 0 ) {
					break;
				}
			}

			order.Logs.Add(new OrderLog {
				Remarks = $"发生退款 {refundRemained:f2} 元",
				IsOpen = true
			});

			if ( setOrderStatus.HasValue && setOrderStatus != order.Status ) {
				order.Logs.Add(new OrderLog {
					FromStatus = order.Status,
					ChangedIntoStatus = setOrderStatus,
					Remarks = $"因退款，订单状态发生变化",
					IsOpen = true
				});
				order.Status = setOrderStatus.Value;
			}

			//先保存一次数据
			await _db.Normalize().SaveChangesAsync();

			//保存之后，再实际请求API进行退款；若退款成功，再改变新增退款记录的状态并保存
			foreach ( var refund in pending ) {
				Result result = null!;

				switch ( refund.SourcePayment.Choise ) {
					case PaymentChoise.WeChat:
						result = _wechat!.PayService().Refund(
							refund.SourcePayment.AppId!,
							refund.SourcePayment.PaymentNo,
							refund.RefundNo,
							refund.SourcePayment.Amount,
							refund.Amount,
							refund.Reason.ToLabel()
						);
						break;

					case PaymentChoise.Alipay:
						result = _alipay!.Refund(
							refund.SourcePayment.PaymentNo,
							refund.RefundNo,
							refund.Amount,
							refund.Reason.ToLabel()
						);
						break;
				}

				refund.Status = result.Ok ? RefundStatus.Refunded : RefundStatus.Checking;
				refund.ResultMessage = result.Message;
			}

			//再次保存数据，更新刚新插入 OrderRefund 的状态
			await _db.Normalize().SaveChangesAsync();

			//返回执行结果
			if ( pending.Any(x => x.Status != RefundStatus.Refunded) ) {
				var choises = string.Join("及", pending.Select(x => x.SourcePayment.Choise).Distinct().ToArray());
				return new Success($"已提交退款，但{choises}返回信息未成功");
			}
			return new Success();
		}
	}
}
