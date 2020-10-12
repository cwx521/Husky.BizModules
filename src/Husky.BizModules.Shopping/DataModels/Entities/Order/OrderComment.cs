using System;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderComment
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		[MaxLength(2000)]
		public string Content { get; set; } = null!;

		[MaxLength(2000)]
		public string? Reply { get; set; }

		public RowStatus Status { get; set; }

		public DateTime? RepliedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public Order Order { get; set; } = null!;
	}
}
