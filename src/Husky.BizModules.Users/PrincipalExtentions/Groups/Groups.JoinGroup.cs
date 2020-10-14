﻿using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public partial class UserGroupsManager
	{
		public async Task<Result> JoinGroup(int groupId) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			_db.Normalize().AddOrUpdate(new UserInGroup {
				UserId = _me.Id,
				GroupId = groupId
			});
			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> JoinGroup(UserGroup userGroup) => await JoinGroup(userGroup.Id);
	}
}