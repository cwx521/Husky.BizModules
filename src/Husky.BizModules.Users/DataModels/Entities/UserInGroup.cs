namespace Husky.BizModules.Users.DataModels
{
	public class UserInGroup
	{
		public int UserId { get; set; }

		public int GroupId { get; set; }


		//nav props

		public User User { get; set; } = null!;
		public UserGroup Group { get; set; } = null!;
	}
}
