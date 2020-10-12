namespace Husky.Principal
{
	public static partial class PrincipalExtensions
	{
		public static UserShoppingCartManager OrderCart(this IPrincipalUser principal) {
			return new UserShoppingCartManager(principal);
		}

		public static UserShoppingOrdersManager ShoppingOrders(this IPrincipalUser principal) {
			return new UserShoppingOrdersManager(principal);
		}
	}
}
