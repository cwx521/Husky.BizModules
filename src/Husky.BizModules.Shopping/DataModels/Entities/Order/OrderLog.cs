using System;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderLog
	{
		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		[StringLength(200), Required]
		public string Remarks { get; set; } = null!;

		public OrderStatus? StatusFrom { get; set; }

		public OrderStatus? StatusChangedTo { get; set; }

		public bool IsPrivate { get; set; }

		public int? CreatedByAdminId { get; set; }

		[StringLength(24)]
		public string? CreatedByAdminName { get; set; }

		public RowStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Order Order { get; set; } = null!;
	}
}
