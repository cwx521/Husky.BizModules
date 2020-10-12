namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductTagRelation
	{
		public int ProductId { get; set; }

		public int ProductTagId { get; set; }


		// nav props

		public Product? Product { get; set; }
		public ProductTag? ProductTag { get; set; }
	}
}
