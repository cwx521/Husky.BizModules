using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;

namespace Husky.Principal
{
	public partial class UserShoppingOrdersManager
	{
		public async Task<Result<Order>> CreateOrderFromCart(PaymentChoise paymentChoise, OrderReceiverAddress addr) {
			return await _me.OrderCart().CreateOrder(paymentChoise, addr);
		}
	}
}
