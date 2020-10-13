using System.Linq;
using Husky.BizModules.Users.DataModels;

namespace Husky.Principal
{
	public partial class UserGroupsManager
	{
		public bool IsInGroup(int groupId) {
			return _me.IsAuthenticated &&
				   _db.UserInGroups.Any(x => x.UserId == _me.Id && x.GroupId == groupId);
		}

		public bool IsInGroup(UserGroup userGroup) => IsInGroup(userGroup.Id);
	}
}