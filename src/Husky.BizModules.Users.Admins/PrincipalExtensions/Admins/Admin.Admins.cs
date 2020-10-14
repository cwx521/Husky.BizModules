using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Users.Admins.DataModels;
using Husky.Principal;
using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Users.Admins.PrincipalExtensions
{
	public partial class AdminsManager
	{
		public async Task<Result> CreateAdmin(int userId) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("没有权限");
			}

			if ( !_db.Admins.Any(x => x.UserId == userId) ) {
				_db.Admins.Add(new Admin {
					UserId = userId
				});
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> DeleteAdmin(int userId, bool suspendInsteadOfDeleting = false) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("没有权限");
			}

			var admin = _db.Admins.Find(userId);
			if ( admin != null ) {
				admin.Status = suspendInsteadOfDeleting ? RowStatus.Suspended : RowStatus.DeletedByAdmin;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> GrantAdmin(int userId, params int[] toBeGrantedRoleIdArray) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("没有权限");
			}

			var admin = _db.Admins.Include(x => x.Roles).Where(x => x.UserId == userId).SingleOrDefault();
			if ( admin == null ) {
				return new Failure("管理用户不存在");
			}

			var excepts = toBeGrantedRoleIdArray.Where(x => !admin.Roles.Any(y => y.Id == x));
			var granting = _db.AdminRoles.Where(x => excepts.Contains(x.Id)).ToArray();
			admin.Roles.AddRange(granting);

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> WithdrawAdmin(int userId, params int[] toBeRemovedRoleIdArray) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("没有权限");
			}

			var admin = _db.Admins.Include(x => x.Roles).Where(x => x.UserId == userId).SingleOrDefault();
			if ( admin == null ) {
				return new Failure("管理用户不存在");
			}

			admin.Roles.RemoveAll(x => toBeRemovedRoleIdArray.Contains(x.Id));

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}
