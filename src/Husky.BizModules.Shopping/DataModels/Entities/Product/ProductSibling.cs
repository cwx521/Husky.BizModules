namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductSibling
	{
		public int ProductId { get; set; }

		public int SiblingProductId { get; set; }


		//nav

		public Product Product { get; set; } = null!;
		public Product SiblingProduct { get; set; } = null!;
	}
}
