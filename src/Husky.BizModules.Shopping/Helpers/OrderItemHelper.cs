using System.Collections.Generic;
using System.Linq;
using Husky.BizModules.Shopping.DataModels;
using Husky.BizModules.Shopping.ViewModels;
using Newtonsoft.Json;

namespace Husky.BizModules.Shopping
{
	public static class OrderItemHelper
	{
		public static List<OrderItemVariationGroup>? GetParsedInstantVariations(this OrderItem orderItem) {
			if ( orderItem.InstantVariationJson == null ) {
				return null;
			}
			return JsonConvert.DeserializeObject<List<OrderItemVariationGroup>>(orderItem.InstantVariationJson);
		}

		public static List<OrderItemVariationGroup>? GetShoppingCartItemVariations(this ShoppingCartItem shoppingCartItem) {
			if ( shoppingCartItem.VariationJson == null ) {
				return null;
			}
			return JsonConvert.DeserializeObject<List<OrderItemVariationGroup>>(shoppingCartItem.VariationJson);
		}

		public static string GetDescription(this List<OrderItemVariationGroup> variations) {
			return string.Join(" | ",
				variations.Select(x =>
					x.Name + "：" +
					string.Join("，",
						x.Variations.Select(y => y.Name)
					)
				)
			);
		}
	}
}
