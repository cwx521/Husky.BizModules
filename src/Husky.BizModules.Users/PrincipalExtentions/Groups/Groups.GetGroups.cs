using System.Linq;
using Husky.BizModules.Users.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserGroupsManager
	{
		public UserGroup[] GetGroups() {
			if ( _me.IsAnonymous ) {
				return new UserGroup[0];
			}

			return _db.UserInGroups
				.AsNoTracking()
				.Where(x => x.UserId == _me.Id)
				.Select(x => x.Group)
				.ToArray();
		}
	}
}