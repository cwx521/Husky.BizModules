using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserShopProductManager
	{
		public async Task<Result> ChangeStatus(int productId, ProductStatus status) {
			if ( _me.IsAnonymous ) {
				return new Failure<Product>("需要先登录");
			}

			var product = _db.Products
				.Include(x => x.Shop).ThenInclude(x => x.Limit)
				.Where(x => x.Id == productId)
				.Where(x => x.Shop.OwnerId == _me.Id)
				.SingleOrDefault();

			if ( product == null ) {
				return new Failure<Product>("未找到对应商品");
			}

			if ( product.Status == status ) {
				return new Success();
			}

			if ( product.Status == ProductStatus.OffShelve ) {
				var shelveCount = _db.Products.Count(x => x.ShopId == product.ShopId && x.Status != ProductStatus.OffShelve);
				if ( product.Shop.Limit.MaxShelveCount <= shelveCount ) {
					return new Failure<Product>($"目前您的店铺限定最多{product.Shop.Limit.MaxShelveCount}个上架商品");
				}
			}

			product.Status = status;
			await _db.Normalize().SaveChangesAsync();

			return new Success();
		}

		private async Task<Result> ChangePropValue<T>(int productId, string propertyName, T propertyValue) {
			if ( _me.IsAnonymous ) {
				return new Failure<Product>("需要先登录");
			}

			var product = _db.Products
				.Where(x => x.Id == productId)
				.Where(x => x.Shop.OwnerId == _me.Id)
				.SingleOrDefault();

			if ( product != null ) {
				typeof(Product).GetProperty(propertyName)!.SetValue(product, propertyValue);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> ChangeName(int productId, string productName) => await ChangePropValue(productId, nameof(Product.ProductName), productName);
		public async Task<Result> ChangeCode(int productId, string? productCode) => await ChangePropValue(productId, nameof(Product.ProductCode), productCode);
		public async Task<Result> ChangeDescription(int productId, string? description) => await ChangePropValue(productId, nameof(Product.Description), description);
		public async Task<Result> ChangePrimaryPicture(int productId, string? primaryPictureUrl) => await ChangePropValue(productId, nameof(Product.PrimaryPictureUrl), primaryPictureUrl);
		public async Task<Result> ChangeActualPrice(int productId, decimal actualPrice) => await ChangePropValue(productId, nameof(Product.ActualPrice), actualPrice);
		public async Task<Result> ChangeOriginalPrice(int productId, decimal originalPrice) => await ChangePropValue(productId, nameof(Product.OriginalPrice), originalPrice);
		public async Task<Result> ChangeStock(int productId, int stock) => await ChangePropValue(productId, nameof(Product.Stock), stock);
		public async Task<Result> ChangeDisplayOnTop(int productId, int displayOnTop) => await ChangePropValue(productId, nameof(Product.InShopDisplayOnTop), displayOnTop);


	}
}
