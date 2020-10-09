using System.ComponentModel.DataAnnotations;
using Husky.BizModules.Users.DataModels;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ShoppingCartItem
	{
		[Key]
		public int Id { get; set; }

		public int BuyerId { get; set; }

		public int ProductId { get; set; }

		[MaxLength(500)]
		public string? ChoiseExpression { get; set; }

		public int Quantity { get; set; }

		public bool Selected { get; set; }

		public bool Removed { get; set; }


		// nav props

		public User Buyer { get; set; } = null!;

		public Product Product { get; set; } = null!;
	}
}
