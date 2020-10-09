using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;

namespace Husky.Principal
{
	partial class UserShoppingCartManager
	{
		public async Task<Result<Order>> CreateOrderFromShoppingCart(PaymentChoise paymentChoise, OrderReceiverAddress addr) {
			return await _me.ShoppingCart().CreateOrder(paymentChoise, addr);
		}
	}
}
