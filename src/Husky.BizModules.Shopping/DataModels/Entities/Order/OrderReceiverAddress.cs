using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderReceiverAddress
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int OrderId { get; set; }

		[StringLength(16), Required]
		public string Province { get; set; } = null!;

		[StringLength(16), Required]
		public string City { get; set; } = null!;

		[StringLength(16)]
		public string? District { get; set; }

		[StringLength(120), Required]
		public string DetailAddress { get; set; } = null!;

		[StringLength(16), Required]
		public string ContactName { get; set; } = null!;

		[StringLength(11), Column(TypeName = "varchar(11)"), Required, Phone]
		public string ContactPhoneNumber { get; set; } = null!;

		[Column(TypeName = "decimal(11, 6)")]
		public decimal? Lon { get; set; }

		[Column(TypeName = "decimal(11, 6)")]
		public decimal? Lat { get; set; }


		// nav props

		public Order Order { get; set; } = null!;


		// calculation

		public string FullAddress => Province + City + District + DetailAddress;
		public string FullAddressSplitBySpace => string.Join(" ", Province, City, District, DetailAddress);
	}
}
