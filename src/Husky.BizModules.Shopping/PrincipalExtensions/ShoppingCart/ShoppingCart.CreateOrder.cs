using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	partial class UserShoppingCartManager
	{
		public async Task<Result<Order>> CreateOrder(PaymentChoise paymentChoise, OrderReceiverAddress addr) {
			var shoppingCartItems = _db.ShoppingCartItems
				.Include(x => x.Product)
				.Where(x => x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.ToList();

			if ( shoppingCartItems.Count == 0 ) {
				return new Failure<Order>("没有选中待结算的商品");
			}

			var outOfStock = shoppingCartItems.FirstOrDefault(x => x.Product.Stock <= x.Quantity);
			if ( outOfStock != null ) {
				return new Failure<Order>("有商品缺货，不足购买数量，请返回购物车查看");
			}

			//Create Order
			var order = new Order {
				BuyerId = _me.Id,
				OrderNo = OrderIdGen.New(),
				ActualTotalAmount = shoppingCartItems.Sum(x => x.Quantity * x.Product.ActualPrice),
				Status = OrderStatus.AwaitPayment
			};

			//Init the first OrderLog of this order
			order.Logs.Add(new OrderLog {
				ChangedIntoStatus = OrderStatus.AwaitPayment,
				Remarks = "确认下单",
				IsOpen = true
			});

			//Set OrderReceiverAddress
			order.ReceiverAddress = new OrderReceiverAddress {
				Province = addr.Province,
				City = addr.City,
				District = addr.District,
				DetailAddress = addr.DetailAddress,
				ContactName = addr.ContactName,
				ContactPhoneNumber = addr.ContactPhoneNumber,
				Lat = addr.Lat,
				Lon = addr.Lon,
			};

			//Move ShoppingCartItems to OrderItems
			order.Items.AddRange(shoppingCartItems.Select(x => new OrderItem {
				ProductId = x.ProductId,
				Quantity = x.Quantity,
				InstantProductCode = x.Product.ProductCode,
				InstantProductName = x.Product.ProductName,
				InstantOriginalPrice = x.Product.OriginalPrice,
				InstantActualPrice = x.Product.ActualPrice,
				InstantVariationExpression = x.VariationExpression,
				InstantVariationDescription = x.VariationDescription,
				Remarks = x.Remarks
			}));

			//Create default OrderPayment
			order.Payments.Add(new OrderPayment {
				PaymentNo = OrderIdGen.New(),
				Amount = order.ActualTotalAmount,
				Choise = paymentChoise,
				Status = PaymentStatus.Await
			});

			//Update database
			_db.Orders.Add(order);
			_db.ShoppingCartItems.RemoveRange(shoppingCartItems);

			//Save & return
			await _db.Normalize().SaveChangesAsync();
			return new Success<Order> {
				Data = order
			};
		}
	}
}
