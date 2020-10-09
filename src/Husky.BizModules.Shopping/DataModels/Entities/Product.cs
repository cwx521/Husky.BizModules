﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		public string ProductName { get; set; } = null!;

		[MaxLength(24), Column(TypeName = "varchar(24)")]
		public string? ProductCode { get; set; }

		[MaxLength(200), Column(TypeName = "varchar(200)")]
		public string? PrimaryImageUrl { get; set; }

		[MaxLength(2000)]
		public string? Description { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal OriginalPrice { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ActualPrice { get; set; }

		public int Stock { get; set; }

		public int DisplayOnTop { get; set; }

		public ProductStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public List<ProductPicture> Pictures { get; set; } = new List<ProductPicture>();
		public List<ProductSibling> Siblings { get; set; } = new List<ProductSibling>();
		public List<ProductChoiseGroup> ChoiseGroups { get; set; } = new List<ProductChoiseGroup>();
		public List<ProductTagRelation> TagRelations { get; set; } = new List<ProductTagRelation>();
	}
}