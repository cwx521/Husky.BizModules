using Alipay.AopSdk.AspnetCore;
using Husky.BizModules.Shopping.DataModels;
using Husky.WeChatIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public sealed partial class UserShoppingOrdersManager
	{
		internal UserShoppingOrdersManager(IPrincipalUser principal) {
			_me = principal;
			_db = principal.ServiceProvider.GetRequiredService<IShoppingDbContext>();
			_wechat = principal.ServiceProvider.GetService<WeChatService>();
			_alipay = principal.ServiceProvider.GetService<AlipayService>();
		}

		private readonly IPrincipalUser _me;
		private readonly IShoppingDbContext _db;
		private readonly WeChatService? _wechat;
		private readonly AlipayService? _alipay;
	}
}
