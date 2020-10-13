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
		public async Task<Result> ChangePropValue<T>(int cartItemId, string cartItemPropertyName, T propertyValue) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var allowedPropertyNames = new[] {
				nameof(OrderCartItem.Selected),
				nameof(OrderCartItem.Removed),
				nameof(OrderCartItem.Remarks),
				nameof(OrderCartItem.Quantity),
				nameof(OrderCartItem.VariationJson),
			};
			if ( !allowedPropertyNames.Contains(cartItemPropertyName) ) {
				return new Failure("不允许修改该字段内容");
			}

			var item = _db.OrderCartItems
				.Where(x => x.BuyerId == _me.Id)
				.Where(x => x.Id == cartItemId)
				.SingleOrDefault();

			if ( item != null ) {
				typeof(Shop).GetProperty(cartItemPropertyName)!.SetValue(item, propertyValue);
				await _db.Normalize().SaveChangesAsync();
			}
			return new Success();
		}

		public async Task<Result> ChangePropValueForAll<T>(string cartItemPropertyName, T propertyValue) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var allowedPropertyNames = new[] {
				nameof(OrderCartItem.Selected),
				nameof(OrderCartItem.Removed),
			};
			if ( !allowedPropertyNames.Contains(cartItemPropertyName) ) {
				return new Failure("不允许修改该字段内容");
			}

			var items = _db.OrderCartItems.Where(x => x.BuyerId == _me.Id).ToList();
			foreach ( var item in items ) {
				typeof(Shop).GetProperty(cartItemPropertyName)!.SetValue(item, propertyValue);
			}

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> SetRemarks(int cartItemId, string? remarks) {
			const int maxLength = 200;
			if ( remarks != null && remarks.Length > maxLength ) {
				return new Failure($"不能超过{maxLength}个字符");
			}
			return await ChangePropValue(cartItemId, nameof(OrderCartItem.Remarks), remarks);
		}

		public async Task<Result> ClearRemarks(int cartItemId) => await SetRemarks(cartItemId, null);

		public async Task<Result> SetQuantity(int cartItemId, int quantity) => await ChangePropValue(cartItemId, nameof(OrderCartItem.Quantity), quantity);
		public async Task<Result> ChangeVariations(int cartItemId, List<OrderItemVariationGroup> variations) => await ChangePropValue(cartItemId, nameof(OrderCartItem.Quantity), JsonConvert.SerializeObject(variations));

		public async Task<Result> Select(int cartItemId) => await ChangePropValue(cartItemId, nameof(OrderCartItem.Selected), true);
		public async Task<Result> Unselect(int cartItemId) => await ChangePropValue(cartItemId, nameof(OrderCartItem.Selected), false);
		public async Task<Result> Remove(int cartItemId) => await ChangePropValue(cartItemId, nameof(OrderCartItem.Removed), true);


		public async Task<Result> SelectAll() => await ChangePropValueForAll(nameof(OrderCartItem.Selected), true);
		public async Task<Result> UnselectAll() => await ChangePropValueForAll(nameof(OrderCartItem.Selected), false);
		public async Task<Result> RemoveAll() => await ChangePropValueForAll(nameof(OrderCartItem.Removed), true);
	}
}
