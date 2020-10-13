using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;

namespace Husky.Principal
{
	public partial class UserShopManager
	{
		public async Task<Result> ChangePropValue<T>(int shopId, string shopPropertyName, T propertyValue) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var allowedPropertyNames = new[] {
				nameof(Shop.ShopName),
				nameof(Shop.Description),
			};
			if ( !allowedPropertyNames.Contains(shopPropertyName) ) {
				return new Failure("不允许修改该字段内容");
			}

			var shop = _db.Shops
				.Where(x => x.Id == shopId)
				.Where(x => x.OwnerId == _me.Id)
				.SingleOrDefault();

			if ( shop != null ) {
				typeof(Shop).GetProperty(shopPropertyName)!.SetValue(shop, propertyValue);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> Rename(int shopId, string newShopName) => await ChangePropValue(shopId, nameof(Shop.ShopName), newShopName);
		public async Task<Result> ChangeDescription(int shopId, string newDescription) => await ChangePropValue(shopId, nameof(Shop.Description), newDescription);
	}
}
