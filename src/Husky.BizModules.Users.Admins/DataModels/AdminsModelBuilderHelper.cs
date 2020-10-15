﻿#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Users.Admins.DataModels
{
	public static class AdminsModelBuilderHelper
	{
		public static void OnUsersModelCreating(this ModelBuilder mb) {

			//Indexes
			mb.Entity<AdminInRole>().HasKey(x => new { x.AdminId, x.RoleId });

			//QueryFilters
			mb.Entity<Admin>().HasQueryFilter(x => x.Status != RowStatus.DeletedByAdmin && x.Status != RowStatus.DeletedByUser);

			//Relationships
			mb.Entity<AdminInRole>(adminInRole => {
				adminInRole.HasOne(x => x.Admin).WithMany(x => x.InRoles).HasForeignKey(x => x.AdminId);
				adminInRole.HasOne(x => x.Role).WithMany(x => x.GrantedToAdmins).HasForeignKey(x => x.RoleId);
			});
		}
	}
}
