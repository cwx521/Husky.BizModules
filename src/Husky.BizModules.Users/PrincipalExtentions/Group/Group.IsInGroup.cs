using Husky.BizModules.Users.DataModels;
using System.Linq;

namespace Husky.Principal
{
	partial class UserGroupManager
	{
		public bool IsInGroup(int groupId) {
			return _me.IsAuthenticated &&
				   _db.UserInGroups.Any(x => x.UserId == _me.Id && x.GroupId == groupId);
		}

		public bool IsInGroup(UserGroup userGroup) => IsInGroup(userGroup.Id);
	}
}