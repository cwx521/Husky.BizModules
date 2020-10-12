//using System.Linq;
//using System.Threading.Tasks;
//using Husky.BizModules.Shopping.DataModels;
//using Microsoft.EntityFrameworkCore;

//namespace Husky.Principal
//{
//	partial class UserShoppingCartManager
//	{
//		public async Task<Result> Select(int shoppingCartItemId) {
//			var item = _db.ShoppingCartItems
//				.Where(x => !x.Selected)
//				.Where(x => x.BuyerId == _me.Id)
//				.Where(x => x.Id == shoppingCartItemId)
//				.SingleOrDefault();

//			if ( item != null ) {
//				item.Selected = true;
//				await _db.Normalize().SaveChangesAsync();
//			}
//			return new Success();
//		}

//		public async Task<Result> Unselect(int shoppingCartItemId) {
//			var item = _db.ShoppingCartItems
//				.Where(x => x.Selected)
//				.Where(x => x.BuyerId == _me.Id)
//				.Where(x => x.Id == shoppingCartItemId)
//				.SingleOrDefault();

//			if ( item != null ) {
//				item.Selected = false;
//				await _db.Normalize().SaveChangesAsync();
//			}
//			return new Success();
//		}

//		public async Task<Result> SelectAll() {
//			var items = _db.ShoppingCartItems
//				.Where(x => !x.Selected)
//				.Where(x => x.BuyerId == _me.Id)
//				.ToList();

//			if ( items.Count != 0 ) {
//				items.ForEach(x => x.Selected = true);
//				await _db.Normalize().SaveChangesAsync();
//			}
//			return new Success();
//		}

//		public async Task<Result> UnselectAll() {
//			var items = _db.ShoppingCartItems
//				.Where(x => x.Selected)
//				.Where(x => x.BuyerId == _me.Id)
//				.ToList();

//			if ( items.Count != 0 ) {
//				items.ForEach(x => x.Selected = false);
//				await _db.Normalize().SaveChangesAsync();
//			}
//			return new Success();
//		}


//		public async Task<Result> Remove(int shoppingCartItemId) {
//			var item = _db.ShoppingCartItems
//				.Where(x => x.BuyerId == _me.Id)
//				.Where(x => x.Id == shoppingCartItemId)
//				.SingleOrDefault();

//			if ( item != null ) {
//				item.Removed = true;
//				await _db.Normalize().SaveChangesAsync();
//			}
//			return new Success();
//		}

//		public async Task<Result> RemoveAll() {
//			var items = _db.ShoppingCartItems
//				.Where(x => x.BuyerId == _me.Id)
//				.ToList();

//			if ( items.Count != 0 ) {
//				items.ForEach(x => x.Removed = true);
//				await _db.Normalize().SaveChangesAsync();
//			}
//			return new Success();
//		}

//		public async Task<Result> AddToCart(int productId, string? variationExpression = null, int quantity = 1) {
//			var product = _db.Products
//				.AsNoTracking()
//				.Where(x => x.Id == productId);

//			if ( product == null ) {
//				return new Failure("商品不存在");
//			}

//			var shoppingCartItem = new ShoppingCartItem {
//				ProductId = productId,
//				BuyerId = _me.Id,
//				BuyerName = _me.DisplayName,
//				VariationExpression = variationExpression,
//				VariationDescription = null,	//todo
//				Quantity = 1
//			};
//		}
//	}
//}
