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
	public partial class UserShoppingCartManager
	{
		public async Task<Result> Select(int orderCartItemId) {
			var item = _db.OrderCartItems
				.Where(x => !x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == orderCartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				item.Selected = true;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> Unselect(int orderCartItemId) {
			var item = _db.OrderCartItems
				.Where(x => x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == orderCartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				item.Selected = false;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> SelectAll() {
			var items = _db.OrderCartItems
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
			var items = _db.OrderCartItems
				.Where(x => x.Selected)
				.Where(x => x.BuyerId == _me.Id)
				.ToList();

			if ( items.Count != 0 ) {
				items.ForEach(x => x.Selected = false);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}


		public async Task<Result> Remove(int orderCartItemId) {
			var item = _db.OrderCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == orderCartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				item.Removed = true;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> RemoveAll() {
			var items = _db.OrderCartItems
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
			var orderCartItem = _db.OrderCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.ProductId == productId)
				.Where(x => x.VariationJson == variationJson)
				.FirstOrDefault();

			if ( orderCartItem == null ) {
				orderCartItem = new OrderCartItem {
					ProductId = productId,
					BuyerId = _me.Id,
					BuyerName = _me.DisplayName,
					VariationJson = variationJson
				};
				_db.OrderCartItems.Add(orderCartItem);
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
			if ( product.Stock == 0 ) {
				return new Failure("商品缺货");
			}

			orderCartItem.Quantity += quantity;
			orderCartItem.Quantity = Math.Min(orderCartItem.Quantity, product.Stock);

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> SetQuantity(int orderCartItemId, int quantity) {
			var orderCartItem = _db.OrderCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == orderCartItemId)
				.SingleOrDefault();

			if ( orderCartItem != null ) {
				orderCartItem.Quantity = quantity;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> SetRemarks(int orderCartItemId, string remarks) {
			var orderCartItem = _db.OrderCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == orderCartItemId)
				.SingleOrDefault();

			const int maxLength = 200;
			if ( remarks.Length > maxLength ) {
				return new Failure($"不能超过{maxLength}个字符");
			}

			if ( orderCartItem != null ) {
				orderCartItem.Remarks = remarks;
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}
	}
}
