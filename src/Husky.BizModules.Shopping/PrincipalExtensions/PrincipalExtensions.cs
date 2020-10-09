using Husky;
using Husky.BizModules.Users.DataModels;
using Husky.Principal.SessionData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Husky.Principal
{
	public static partial class PrincipalExtensions
	{
		public static UserShoppingCartManager ShoppingCart(this IPrincipalUser principal) {
			return new UserShoppingCartManager(principal);
		}
	}
}
