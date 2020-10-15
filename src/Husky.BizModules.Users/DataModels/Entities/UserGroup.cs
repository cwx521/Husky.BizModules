using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Users.DataModels
{
	public class UserGroup
	{
		public int Id { get; set; }

		[StringLength(50)]
		public string GroupName { get; set; } = null!;
	}
}
