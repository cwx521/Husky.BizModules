using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderExpress
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		public OrderExpressDirection Direction { get; set; }

		[MaxLength(16), Column(TypeName = "varchar(15)")]
		public string ExpressNo { get; set; } = null!;

		[MaxLength(16)]
		public string? ExpressCompany { get; set; }

		[MaxLength(2000)]
		public string? ExpressQueryResult { get; set; }

		[MaxLength(200)]
		public string? Remarks { get; set; }

		public DateTime? ExpressQueryUpdatedTime { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		[JsonIgnore]
		public Order Order { get; set; } = null!;
	}
}
