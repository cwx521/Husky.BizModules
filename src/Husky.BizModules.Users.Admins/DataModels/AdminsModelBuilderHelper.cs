#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Users.Admins.DataModels
{
	public static class AdminsModelBuilderHelper
	{
		public static void OnUsersModelCreating(this ModelBuilder mb) {

			mb.Entity<Admin>(admin => {
				admin.HasQueryFilter(x => x.Status != RowStatus.DeletedByAdmin && x.Status != RowStatus.DeletedByUser);
				//admin.HasMany(x => x.Roles).WithMany(x => x.Admins);
			});
		}
	}
}
