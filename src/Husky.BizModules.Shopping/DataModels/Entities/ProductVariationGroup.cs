﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductVariationGroup
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }

		[MaxLength(16)]
		public string GroupName { get; set; } = null!;

		public int MinRequired { get; set; }

		public int MaxAllowed { get; set; }

		public RowStatus Status { get; set; }


		//nav props

		public List<ProductVariation> Variations { get; set; } = new List<ProductVariation>();
	}
}