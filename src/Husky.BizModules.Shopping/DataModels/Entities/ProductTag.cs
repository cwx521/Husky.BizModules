using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductTag
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(24)]
		public string TagName { get; set; } = null!;

		public int DisplayOnTop { get; set; }

		public bool AsMenuItem { get; set; } = true;


		// nav props

		public List<ProductTagRelation> ProductRelations { get; set; } = new List<ProductTagRelation>();
	}
}
