using System;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public class UserInfoViewModel
	{
		public string? PhoneNumber { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhotoUrl { get; set; }
		public DateTime RegisteredTime { get; set; }
		public bool IsTwoFactorValidated { get; set; }
		public ActionAwait AwaitChangePassword { get; internal set; }
	}
}
