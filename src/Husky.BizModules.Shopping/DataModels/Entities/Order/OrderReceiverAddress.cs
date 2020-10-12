﻿using System.ComponentModel.DataAnnotations;
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

		[MaxLength(120)]
		public string DetailAddress { get; set; } = null!;

		[MaxLength(16)]
		public string ContactName { get; set; } = null!;

		[MaxLength(11), Column(TypeName = "varchar(11)"), Phone]
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