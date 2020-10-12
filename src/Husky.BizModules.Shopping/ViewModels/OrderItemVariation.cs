using System.Collections.Generic;

namespace Husky.BizModules.Shopping.ViewModels
{
	public class OrderItemVariationGroup
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public List<OrderItemVariation> Variations { get; set; } = new List<OrderItemVariation>();
	}

	public class OrderItemVariation
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
	}
}
