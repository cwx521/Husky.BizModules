using System;

namespace Husky.Principal
{
	public class UserQuickView
	{
		public string? PhoneNumber { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhotoUrl { get; set; }
		public DateTime RegisteredTime { get; set; }

		public ActionAwait AwaitChangePassword { get; internal set; }
	}
}
