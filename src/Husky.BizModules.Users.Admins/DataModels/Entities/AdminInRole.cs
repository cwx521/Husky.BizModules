namespace Husky.BizModules.Users.Admins.DataModels
{
	public class AdminInRole
	{
		public int AdminId { get; set; }

		public int RoleId { get; set; }


		// nav props

		public Admin Admin { get; set; } = null!;
		public AdminRole Role { get; set; } = null!;
	}
}
