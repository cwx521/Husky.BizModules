using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ProductChoise
	{
		[Key]
		public int Id { get; set; }

		public int ChoiseGroupId { get; set; }

		[MaxLength(24)]
		public string ChoiseName { get; set; } = null!;

		[MaxLength(200), Column(TypeName = "varchar(200)")]
		public string? ChoiseImageUrl { get; set; }

		public RowStatus Status { get; set; }
	}
}