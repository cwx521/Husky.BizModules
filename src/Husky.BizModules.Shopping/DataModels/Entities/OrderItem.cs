using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderItem
	{
		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int ProductId { get; set; }

		[MaxLength(24), Column(TypeName = "varchar(24)")]
		public string? InstantProductCode { get; set; }

		[MaxLength(50)]
		public string? InstantProductName { get; set; }

		[MaxLength(100)]
		public string? InstantVariationExpression { get; set; }

		[MaxLength(500)]
		public string? InstantVariationDescription { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal InstantOriginalPrice { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal InstantActualPrice { get; set; }

		public int Quantity { get; set; }

		public int ReturnedQuantity { get; set; }

		[MaxLength(200)]
		public string? Remarks { get; set; }


		// nav props

		[JsonIgnore]
		public Order Order { get; set; } = null!;

		public Product Product { get; set; } = null!;


		// calculated

		public decimal InstantDiscounted => InstantActualPrice / InstantOriginalPrice;
		public decimal InstantDiscount => 1m - InstantDiscounted;
		public decimal QuantitizedOriginalAmount => InstantOriginalPrice * Quantity;
		public decimal QuantitizedActualAmount => InstantActualPrice * Quantity;
	}
}
