using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Husky.BizModules.Users.Admins.DataModels
{
	public class AdminRole
	{
		[Key]
		public int Id { get; set; }

		[Unique]
		public string RoleName { get; set; } = null!;

		public long Powers { get; set; }


		// nav props

		public List<AdminInRole> GrantedToAdmins { get; set; } = new List<AdminInRole>();
	}
}
