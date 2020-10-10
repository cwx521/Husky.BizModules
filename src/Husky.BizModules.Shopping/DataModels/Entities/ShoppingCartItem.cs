using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ShoppingCartItem
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }

		public int BuyerId { get; set; }

		[MaxLength(36)]
		public string BuyerName { get; set; } = null!;

		[MaxLength(100)]
		public string? ChoiseExpression { get; set; }

		[MaxLength(500)]
		public string? ChoiseDescription { get; set; }

		public int Quantity { get; set; }

		[MaxLength(200)]
		public string? Remarks { get; set; }

		public bool Selected { get; set; }

		public bool Removed { get; set; }


		// nav props

		public Product Product { get; set; } = null!;
	}
}
