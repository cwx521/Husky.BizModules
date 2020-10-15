using System.Linq;
using Husky;
using Husky.BizModules.Users.Admins.DataModels;
using Husky.BizModules.Users.Admins.PrincipalExtentions;
using Husky.Principal.SessionData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public static partial class PrincipalExtension
	{
		public static AdminInfoViewModel Admin(this IPrincipalUser principal) {
			if ( principal.Id == 0 || !(principal.SessionData() is SessionDataContainer sessionData) ) {
				return new AdminInfoViewModel {
					IsAdmin = false,
					Roles = new string[0]
				};
			}

			return (AdminInfoViewModel)sessionData.GetOrAdd(nameof(AdminInfoViewModel), key => {
				using var scope = principal.ServiceProvider.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<IAdminsDbContext>();

				var roles = db.AdminRoles
					.AsNoTracking()
					.Where(x => x.GrantedToAdmins.Any(y =>
						y.Admin.UserId == principal.Id &&
						y.Admin.Status == RowStatus.Active))
					.ToList();

				return new AdminInfoViewModel {
					IsAdmin = roles.Count != 0,
					Roles = roles.Select(x => x.RoleName).ToArray(),
					Powers = roles.Aggregate((long)0, (i, x) => i | x.Powers)
				};
			});
		}
	}
}
