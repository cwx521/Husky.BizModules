using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Shopping.DataModels;
using Husky.BizModules.Shopping.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Husky.Principal
{
	partial class UserShoppingCartManager
	{
		public async Task<Result> Select(int shoppingCartItemId) {
			var item = _db.ShoppingCartItems
				.Where(x => !x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == shoppingCartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				item.Selected = true;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> Unselect(int shoppingCartItemId) {
			var item = _db.ShoppingCartItems
				.Where(x => x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == shoppingCartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				item.Selected = false;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> SelectAll() {
			var items = _db.ShoppingCartItems
				.Where(x => !x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.ToList();

			if ( items.Count != 0 ) {
				items.ForEach(x => x.Selected = true);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> UnselectAll() {
			var items = _db.ShoppingCartItems
				.Where(x => x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.ToList();

			if ( items.Count != 0 ) {
				items.ForEach(x => x.Selected = false);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}


		public async Task<Result> Remove(int shoppingCartItemId) {
			var item = _db.ShoppingCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == shoppingCartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				item.Removed = true;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> RemoveAll() {
			var items = _db.ShoppingCartItems
				.Where(x => x.BuyerId == _me.Id)
				.ToList();

			if ( items.Count != 0 ) {
				items.ForEach(x => x.Removed = true);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> AddToCart(int productId, List<OrderItemVariationGroup> orderItemVariations, int quantity = 1) {
			var variationJson = JsonConvert.SerializeObject(orderItemVariations);
			var shoppingCartItem = _db.ShoppingCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.ProductId == productId)
				.Where(x => x.VariationJson == variationJson)
				.FirstOrDefault();

			if ( shoppingCartItem == null ) {
				shoppingCartItem = new ShoppingCartItem {
					ProductId = productId,
					BuyerId = _me.Id,
					BuyerName = _me.DisplayName,
					VariationJson = variationJson
				};
				_db.ShoppingCartItems.Add(shoppingCartItem);
			}

			var product = _db.Products
				.AsNoTracking()
				.Include(x => x.VariationGroups).ThenInclude(x => x.Variations)
				.Where(x => x.Id == productId)
				.SingleOrDefault();

			if ( product == null ) {
				return new Failure("商品不存在");
			}
			if ( product.Status != ProductStatus.Active ) {
				return new Failure($"商品{product.Status.ToLabel()}，无法购买");
			}
			if ( product.OffShelveTime.HasValue && product.OffShelveTime.Value < DateTime.Now ) {
				return new Failure("商品已过期下架");
			}
			if ( product.Stock == 0) {
				return new Failure("商品缺货");
			}

			shoppingCartItem.Quantity += quantity;
			shoppingCartItem.Quantity = Math.Min(shoppingCartItem.Quantity, product.Stock);

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> SetQuantity(int shoppingCartItemId, int quantity) {
			var shoppingCartItem = _db.ShoppingCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == shoppingCartItemId)
				.SingleOrDefault();

			if ( shoppingCartItem != null ) {
				shoppingCartItem.Quantity = quantity;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> SetRemarks(int shoppingCartItemId, string remarks) {
			var shoppingCartItem = _db.ShoppingCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == shoppingCartItemId)
				.SingleOrDefault();

			const int maxLength = 200;
			if ( remarks.Length > maxLength ) {
				return new Failure($"不能超过{maxLength}个字符");
			}

			if ( shoppingCartItem != null ) {
				shoppingCartItem.Remarks = remarks;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}
	}
}
