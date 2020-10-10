using Alipay.AopSdk.AspnetCore;
using Husky.BizModules.Shopping.DataModels;
using Husky.WeChatIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public sealed partial class UserShoppingOrdersManager
	{
		internal UserShoppingOrdersManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IShoppingDbContext>();
			_http = principal.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
			_wechat = principal.ServiceProvider.GetService<WeChatService>();
			_alipay = principal.ServiceProvider.GetService<AlipayService>();
		}

		private readonly IPrincipalUser _me;
		private readonly IShoppingDbContext _db;
		private readonly HttpContext _http;
		private readonly WeChatService? _wechat;
		private readonly AlipayService? _alipay;
	}
}
