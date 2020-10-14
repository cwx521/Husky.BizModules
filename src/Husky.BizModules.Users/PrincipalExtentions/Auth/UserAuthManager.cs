using Husky.BizModules.Users.DataModels;
using Husky.Principal;
using Husky.WeChatIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public sealed partial class UserAuthManager
	{
		internal UserAuthManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IUsersDbContext>();
			_http = principal.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
			_wechat = principal.ServiceProvider.GetService<WeChatService>();
		}

		private readonly IPrincipalUser _me;
		private readonly IUsersDbContext _db;
		private readonly HttpContext _http;
		private readonly WeChatService? _wechat;
	}
}
