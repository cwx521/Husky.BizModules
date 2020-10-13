namespace Husky.Principal
{
	public static partial class PrincipalExtensions
	{
		public static UserSellingManager Shop(this IPrincipalUser principal) {
			return new UserSellingManager(principal);
		}

		public static UserShopManager Selling(this IPrincipalUser principal) {
			return new UserShopManager(principal);
		}

		public static UserCartManager ShoppingCart(this IPrincipalUser principal) {
			return new UserCartManager(principal);
		}

		public static UserOrdersManager ShoppingOrders(this IPrincipalUser principal) {
			return new UserOrdersManager(principal);
		}
	}
}
