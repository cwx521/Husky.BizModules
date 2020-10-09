using Newtonsoft.Json;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductTagRelation
	{
		public int ProductId { get; set; }

		public int ProductTagId { get; set; }


		// nav props

		[JsonIgnore]
		public Product Product { get; set; } = null!;

		public ProductTag ProductTag { get; set; } = null!;
	}
}
