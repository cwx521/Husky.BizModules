using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserShopProductManager
	{
		public async Task<Result<Product>> CreateProduct(string productName, int stock, decimal actualPrice, decimal? originalPrice = null) {
			if ( _me.IsAnonymous ) {
				return new Failure<Product>("需要先登录");
			}

			var shop = _db.Shops
				.AsNoTracking()
				.Include(x => x.Limit)
				.Where(x => x.OwnerId == _me.Id)
				.FirstOrDefault();

			if ( shop == null ) {
				return new Failure<Product>("还没有创建店铺");
			}
			if ( shop.Status != RowStatus.Active ) {
				return new Failure<Product>("还没有开通店铺");
			}
			if ( shop.Limit.MaxSinglePrice < actualPrice ) {
				return new Failure<Product>($"目前您的店铺单品价格限额不能高于{shop.Limit.MaxSinglePrice:f2}元");
			}

			var shelveCount = _db.Products.Count(x => x.ShopId == shop.Id && x.Status != ProductStatus.OffShelve);
			if ( shop.Limit.MaxShelveCount <= shelveCount ) {
				return new Failure<Product>($"目前您的店铺限定最多{shop.Limit.MaxShelveCount}个上架商品");
			}

			var product = new Product {
				ShopId = shop.Id,
				ProductName = productName,
				Stock = stock,
				ActualPrice = actualPrice,
				OriginalPrice = originalPrice ?? actualPrice
			};
			_db.Products.Add(product);

			await _db.Normalize().SaveChangesAsync();
			return new Success<Product>(product);
		}
	}
}
