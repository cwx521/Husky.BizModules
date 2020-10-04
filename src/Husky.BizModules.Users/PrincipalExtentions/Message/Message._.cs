﻿using Husky.BizModules.Users.DataModels;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public sealed partial class UserMessageManager
	{
		internal UserMessageManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IUsersDbContext>();
		}

		private readonly IPrincipalUser _me;
		private readonly IUsersDbContext _db;
	}
}
