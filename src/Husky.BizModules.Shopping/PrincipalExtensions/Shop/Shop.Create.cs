using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Husky.Principal
{
	public partial class UserShopManager
	{
		public async Task<Result<Shop>> Create(string shopName, string description, RowStatus status = RowStatus.Active) {
			if ( _me.IsAnonymous ) {
				return new Failure<Shop>("需要先登录");
			}

			var hasAlready = _db.Shops.IgnoreQueryFilters().Any(x => x.OwnerId == _me.Id);
			if ( hasAlready ) {
				return new Failure<Shop>("只能申请开通一个店铺");
			}

			var shop = new Shop {
				OwnerId = _me.Id,
				OwnerName = _me.DisplayName,
				ShopName = shopName,
				Description = description,
				Status = status,
				Limit = new ShopLimit()
			};
			_db.Shops.Add(shop);

			await _db.Normalize().SaveChangesAsync();
			return new Success<Shop>(shop);
		}
	}
}
