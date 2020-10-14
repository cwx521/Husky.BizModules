using System.Linq;
using Husky;
using Husky.BizModules.Users.Admins.DataModels;
using Husky.Principal.SessionData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public static partial class PrincipalExtensions
	{
		public static AdminInfoViewModel AdminInfo(this IPrincipalAdmin principal) {
			if ( principal.Id == 0 || !(principal.SessionData() is SessionDataContainer sessionData) ) {
				principal.Auth().SignOut();
				return new AdminInfoViewModel {
					IsAdmin = false,
					Roles = new string[0]
				};
			}

			return (AdminInfoViewModel)sessionData.GetOrAdd(nameof(AdminInfoViewModel), key => {
				using var scope = principal.ServiceProvider.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<IAdminsDbContext>();

				var roles = db.Admins
					.AsNoTracking()
					.Where(x => x.UserId == principal.Id)
					.Where(x => x.Status == RowStatus.Active)
					.SelectMany(x => x.Roles)
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
