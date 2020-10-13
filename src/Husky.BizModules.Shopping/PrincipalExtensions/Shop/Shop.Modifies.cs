using System.Linq;
using System.Threading.Tasks;

namespace Husky.Principal
{
	public partial class UserShopManager
	{
		public async Task<Result> Rename(int shopId, string newShopName) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var shop = _db.Shops
				.Where(x => x.Id == shopId)
				.Where(x => x.OwnerId == _me.Id)
				.SingleOrDefault();

			if ( shop != null ) {
				shop.ShopName = newShopName;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> ChangeDescription(int shopId, string newDescription) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var shop = _db.Shops
				.Where(x => x.Id == shopId)
				.Where(x => x.OwnerId == _me.Id)
				.SingleOrDefault();

			if ( shop != null ) {
				shop.Description = newDescription;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}
	}
}
