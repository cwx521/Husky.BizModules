﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace Husky.Principal
{
	public partial class UserProfileManager
	{
		public async Task<Result> UseDisplayName(string displayName, bool allowDuplication) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}
			if ( displayName == null ) {
				return new Failure("不能为空");
			}
			if ( displayName.Length > 18 ) {
				return new Failure("不能超过18个字符");
			}
			if ( !allowDuplication ) {
				var isTaken = _db.Users.Any(x => x.Id != _me.Id && x.DisplayName == displayName);
				if ( isTaken ) {
					return new Failure("此名字已经被别人占用");
				}
			}

			var user = _db.Users.Find(_me.Id);
			user.DisplayName = displayName;

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}