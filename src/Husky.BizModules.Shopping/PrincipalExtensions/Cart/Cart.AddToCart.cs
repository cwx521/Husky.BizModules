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
	public partial class UserCartManager
	{
		public async Task<Result<OrderCartItem>> AddToCart(int productId, List<OrderItemVariationGroup> orderItemVariations, int quantity = 1) {
			if ( _me.IsAnonymous ) {
				return new Failure<OrderCartItem>("需要先登录");
			}

			var variationJson = JsonConvert.SerializeObject(orderItemVariations);
			var cartItem = _db.OrderCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.ProductId == productId)
				.Where(x => x.VariationJson == variationJson)
				.FirstOrDefault();

			if ( cartItem == null ) {
				cartItem = new OrderCartItem {
					ProductId = productId,
					BuyerId = _me.Id,
					BuyerName = _me.DisplayName,
					VariationJson = variationJson
				};
				_db.OrderCartItems.Add(cartItem);
			}

			var product = _db.Products
				.AsNoTracking()
				.Include(x => x.VariationGroups).ThenInclude(x => x.Variations)
				.Where(x => x.Id == productId)
				.SingleOrDefault();

			if ( product == null ) {
				return new Failure<OrderCartItem>("商品不存在");
			}
			if ( product.Status != ProductStatus.Active ) {
				return new Failure<OrderCartItem>($"商品{product.Status.ToLabel()}，无法购买");
			}
			if ( product.OffShelveTime < DateTime.Now ) {
				return new Failure<OrderCartItem>("商品已过期下架");
			}
			if ( product.Stock == 0 ) {
				return new Failure<OrderCartItem>("商品缺货");
			}

			cartItem.Quantity += quantity;
			cartItem.Quantity = Math.Min(cartItem.Quantity, product.Stock);

			await _db.Normalize().SaveChangesAsync();
			return new Success<OrderCartItem>(cartItem);
		}

	}
}
