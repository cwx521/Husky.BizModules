using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		public int ShopId { get; set; }

		[MaxLength(100)]
		public string ProductName { get; set; } = null!;

		[MaxLength(24), Column(TypeName = "varchar(24)")]
		public string? ProductCode { get; set; }

		[MaxLength(200), Column(TypeName = "varchar(200)")]
		public string? PrimaryPictureUrl { get; set; }

		[MaxLength(2000)]
		public string? Description { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ActualPrice { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal OriginalPrice { get; set; }

		[Column(TypeName = "decimal(6,2)")]
		public decimal? ExpectedTip { get; set; }

		public int Stock { get; set; }

		public int SoldCount { get; set; }

		public int RatedCount { get; set; }

		[Column(TypeName = "decimal(2,1)")]
		public decimal RatedAverage { get; set; }

		public int DisplayOnTop { get; set; }

		public ProductStatus Status { get; set; }

		public DateTime OffShelveTime { get; set; } = DateTime.Now.AddMonths(1);

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Shop Shop { get; set; } = null!;
		public List<ProductPicture> Pictures { get; set; } = new List<ProductPicture>();
		public List<ProductVariationGroup> VariationGroups { get; set; } = new List<ProductVariationGroup>();
		public List<ProductTagRelation> TagRelations { get; set; } = new List<ProductTagRelation>();
		public List<ProductSibling> Siblings { get; set; } = new List<ProductSibling>();


		// calculation

		public decimal Discounted => ActualPrice / OriginalPrice;
		public decimal Discount => 1m - Discounted;
	}
}
