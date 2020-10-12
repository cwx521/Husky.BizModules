using System;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderLog
	{
		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		public OrderStatus? FromStatus { get; set; }

		public OrderStatus? ChangedIntoStatus { get; set; }

		[MaxLength(200)]
		public string? Remarks { get; set; }

		public bool IsOpen { get; set; }

		public int? ByAdminId { get; set; }

		[MaxLength(24)]
		public string? ByAdminName { get; set; }

		public RowStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Order? Order { get; set; }
	}
}
