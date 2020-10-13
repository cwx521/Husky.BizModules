using Husky.BizModules.Shopping.DataModels;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public sealed partial class UserShopManager
	{
		internal UserShopManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IShoppingDbContext>();
		}

		private readonly IPrincipalUser _me;
		private readonly IShoppingDbContext _db;
	}
}
