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
		public string? ConcurrentProductCode { get; set; }

		[MaxLength(50)]
		public string? ConcurrentProductName { get; set; }

		[MaxLength(500)]
		public string? ConcurrentChoiseExpression { get; set; }

		[MaxLength(500)]
		public string? ConcurrentChoiseDescription { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ConcurrentOriginalPrice { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ConcurrentActualPrice { get; set; }

		public int Quantity { get; set; }

		public int ReturnedQuantity { get; set; }

		[MaxLength(200)]
		public string? Remarks { get; set; }


		// nav props

		[JsonIgnore]
		public Order Order { get; set; } = null!;

		public Product Product { get; set; } = null!;


		// calculated

		public decimal ConcurrentDiscounted => ConcurrentActualPrice / ConcurrentOriginalPrice;
		public decimal ConcurrentDiscount => 1m - ConcurrentDiscounted;
		public decimal QuantitizedOriginalAmount => ConcurrentOriginalPrice * Quantity;
		public decimal QuantitizedActualAmount => ConcurrentActualPrice * Quantity;
	}
}
