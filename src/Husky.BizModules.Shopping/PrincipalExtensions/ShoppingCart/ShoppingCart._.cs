using Husky.BizModules.Shopping.DataModels;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public sealed partial class UserShoppingCartManager
	{
		internal UserShoppingCartManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IShoppingDbContext>();
		}

		private readonly IPrincipalUser _me;
		private readonly IShoppingDbContext _db;


		//private async Task<Result> SetAndSaveOrderCartItemPropertyValue<T>(int orderCartItemId, string propertyName, T propertyValue) {
		//	var orderCartItem = _db.OrderCartItems
		//		.Where(x => x.BuyerId == _me.Id)
		//		.Where(x => x.Id == orderCartItemId)
		//		.SingleOrDefault();

		//	if ( orderCartItem != null ) {
		//		typeof(OrderCartItem).GetProperty(propertyName)!.SetValue(orderCartItem, propertyValue);
		//		await _db.Normalize().SaveChangesAsync();
		//	}
		//	return new Success();
		//}
	}
}
