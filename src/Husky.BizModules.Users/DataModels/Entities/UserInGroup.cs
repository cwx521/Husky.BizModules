namespace Husky.BizModules.Users.DataModels
{
	public class UserInGroup
	{
		public int UserId { get; set; }

		public int GroupId { get; set; }


		//nav props

		public UserGroup? Group { get; set; }
	}
}
