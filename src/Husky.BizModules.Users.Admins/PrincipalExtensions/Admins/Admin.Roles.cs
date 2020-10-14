using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Users.Admins.DataModels;
using Husky.Principal;
using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Users.Admins.PrincipalExtensions
{
	public partial class AdminsManager
	{
		public async Task<Result<AdminRole>> CreateRole(string roleName, long powers) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure<AdminRole>("没有权限");
			}
			if ( _db.AdminRoles.Any(x => x.RoleName == roleName) ) {
				return new Failure<AdminRole>($"管理角色组名“{roleName}”已存在");
			}
			var adminRole = new AdminRole {
				RoleName = roleName,
				Powers = powers
			};
			_db.AdminRoles.Add(adminRole);

			await _db.Normalize().SaveChangesAsync();
			return new Success<AdminRole>(adminRole);
		}

		public async Task<Result> ChangeRolePropValue<T>(int roleId, string adminRolePropertyName, T propertyValue) {
			if ( _me.IsAnonymous || !_me.AdminInfo().IsAdmin ) {
				return new Failure("没有权限");
			}

			var allowedPropertyNames = new[] {
				nameof(AdminRole.RoleName),
				nameof(AdminRole.Powers)
			};
			if ( !allowedPropertyNames.Contains(adminRolePropertyName) ) {
				return new Failure("不允许修改该字段内容");
			}

			var adminRole = _db.AdminRoles.Find(roleId);
			if ( adminRole != null ) {
				typeof(AdminRole).GetProperty(adminRolePropertyName)!.SetValue(adminRole, propertyValue);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> ChangeRoleName(int roleId, string roleName) => await ChangeRolePropValue(roleId, nameof(AdminRole.RoleName), roleName);
		public async Task<Result> ChangeRolePowersAssignment(int roleId, long powers) => await ChangeRolePropValue(roleId, nameof(AdminRole.Powers), powers);
		public async Task<Result> DeleteRole(int roleId) => await ChangeRolePropValue(roleId, nameof(AdminRole.Status), RowStatus.DeletedByAdmin);
	}
}
