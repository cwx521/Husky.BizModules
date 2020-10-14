using Husky.BizModules.Users.DataModels;
using Husky.Principal;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public sealed partial class UserProfileManager
	{
		internal UserProfileManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IUsersDbContext>();
		}

		private readonly IPrincipalUser _me;
		private readonly IUsersDbContext _db;
	}
}
