using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderReceiverAddress
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int OrderId { get; set; }

		[MaxLength(16)]
		public string Province { get; set; } = null!;

		[MaxLength(16)]
		public string City { get; set; } = null!;

		[MaxLength(16)]
		public string? District { get; set; }

		[MaxLength(100)]
		public string DetailAddress { get; set; } = null!;

		[MaxLength(16)]
		public string ContactName { get; set; } = null!;

		[MaxLength(11), Phone, Column(TypeName = "varchar(11)")]
		public string ContactPhoneNumber { get; set; } = null!;

		[Column(TypeName = "decimal(11, 6)")]
		public decimal? Lon { get; set; }

		[Column(TypeName = "decimal(11, 6)")]
		public decimal? Lat { get; set; }


		// Order

		public Order? Order { get; set; }


		// calculation

		public string FullAddress => Province + City + District + DetailAddress;
		public string FullAddressSplitBySpace => string.Join(" ", Province, City, District, DetailAddress);
	}
}
