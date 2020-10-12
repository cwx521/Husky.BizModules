using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ShopLimit
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ShopId { get; set; }

		public int MaxProductCount { get; set; } = 20;

		public decimal MaxSinglePrice { get; set; } = 500;


		// nav props

		public Shop? Shop { get; set; }
	}
}
