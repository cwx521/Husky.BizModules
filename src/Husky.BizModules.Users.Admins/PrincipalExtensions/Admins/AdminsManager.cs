using Husky.BizModules.Users.Admins.DataModels;
using Husky.Principal;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.BizModules.Users.Admins.PrincipalExtensions
{
	public sealed partial class AdminsManager
	{
		internal AdminsManager(IPrincipalAdmin principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IAdminsDbContext>();
		}

		private readonly IPrincipalAdmin _me;
		private readonly IAdminsDbContext _db;
	}
}
