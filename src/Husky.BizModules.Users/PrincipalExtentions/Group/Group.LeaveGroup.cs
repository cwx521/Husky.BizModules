using Husky.BizModules.Users.DataModels;
using System.Threading.Tasks;

namespace Husky.Principal
{
	partial class UserGroupManager
	{
		public async Task<Result> LeaveGroup(int groupId) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var row = _db.UserInGroups.Find(_me.Id, groupId);
			if ( row != null ) {
				_db.UserInGroups.Remove(row);
			}
			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> LeaveGroup(UserGroup userGroup) => await LeaveGroup(userGroup.Id);
	}
}