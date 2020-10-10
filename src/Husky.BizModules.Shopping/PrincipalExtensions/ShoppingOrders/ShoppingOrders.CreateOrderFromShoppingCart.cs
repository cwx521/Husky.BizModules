using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;

namespace Husky.Principal
{
	partial class UserShoppingOrdersManager
	{
		public async Task<Result<Order>> CreateOrderFromShoppingCart(PaymentChoise paymentChoise, OrderReceiverAddress addr) {
			return await _me.ShoppingCart().CreateOrder(paymentChoise, addr);
		}
	}
}
