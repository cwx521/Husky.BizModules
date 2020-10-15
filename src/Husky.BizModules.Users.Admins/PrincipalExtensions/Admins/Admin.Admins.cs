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
				return new Failure("只允许管理员操作");
			}

			var admin = _db.Admins.Include(x => x.InRoles).Where(x => x.UserId == userId).SingleOrDefault();
			if ( admin == null ) {
				admin = new Admin {
					UserId = userId
				};
				_db.Admins.Add(admin);
			}
			else if ( admin.Status == RowStatus.DeletedByAdmin ) {
				admin.InRoles.Clear();
			}

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> DeleteAdmin(int userId, bool suspendInsteadOfDeleting = false) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("只允许管理员操作");
			}

			var admin = _db.Admins.Find(userId);
			if ( admin != null ) {
				admin.Status = suspendInsteadOfDeleting ? RowStatus.Suspended : RowStatus.DeletedByAdmin;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> SetAdminRoles(int userId, params int[] adminRoleIdArray) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("只允许管理员操作");
			}

			var admin = _db.Admins.Include(x => x.InRoles).Where(x => x.UserId == userId).SingleOrDefault();
			if ( admin == null ) {
				return new Failure("管理员用户不存在");
			}

			var grantingRoleIdArray = adminRoleIdArray.Where(x => !admin.InRoles.Any(y => y.RoleId == x));
			var granting = _db.AdminInRoles.Where(x => grantingRoleIdArray.Contains(x.RoleId)).ToArray();
			admin.InRoles.AddRange(granting);
			admin.InRoles.RemoveAll(x => !adminRoleIdArray.Contains(x.RoleId));

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> GrantAdminRole(int userId, int adminRoleId) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("只允许管理员操作");
			}

			if ( !_db.Admins.Any(x => x.UserId == userId) ) {
				return new Failure("管理员用户不存在");
			}

			_db.Normalize().AddOrUpdate(new AdminInRole {
				AdminId = userId,
				RoleId = adminRoleId
			});
			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> WithdrawAdminRole(int userId, int adminRoleId) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("只允许管理员操作");
			}

			var row = _db.AdminInRoles.SingleOrDefault(x => x.AdminId == userId && x.RoleId == adminRoleId);
			if ( row != null ) {
				_db.AdminInRoles.Remove(row);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}
	}
}
