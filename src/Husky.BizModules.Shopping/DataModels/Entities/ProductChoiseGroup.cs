using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductChoiseGroup
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

		public List<ProductChoise> Choises { get; set; } = new List<ProductChoise>();
	}
}
