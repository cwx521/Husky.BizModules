using System.Linq;
using Husky;
using Husky.BizModules.Users.DataModels;
using Husky.BizModules.Users.PrincipalExtentions;
using Husky.Principal.SessionData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Husky.Principal
{
	public static partial class PrincipalExtension
	{
		public static UserInfoViewModel UserInfo(this IPrincipalUser principal) {
			if ( principal.Id == 0 || !(principal.SessionData() is SessionDataContainer sessionData) ) {
				principal.Auth().SignOut();
				return new UserInfoViewModel();
			}

			return (UserInfoViewModel)sessionData.GetOrAdd(nameof(UserInfoViewModel), key => {
				using var scope = principal.ServiceProvider.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<IUsersDbContext>();

				var quickView = db.Users
					.AsNoTracking()
					.Where(x => x.Id == principal.Id)
					.Select(x => new UserInfoViewModel {
						PhotoUrl = x.PhotoUrl ?? (x.WeChat == null ? null : x.WeChat.HeadImageUrl),
						PhoneNumber = x.Phone == null ? null : x.Phone.Number,
						EmailAddress = x.Email == null ? null : x.Email.EmailAddress,
						RegisteredTime = x.RegisteredTime,
						AwaitChangePassword = ActionAwait.NoNeed,
						IsTwoFactorValidated = false
					})
					.SingleOrDefault();

				if ( quickView == null ) {
					principal.Auth().SignOut();
				}
				return quickView ?? new UserInfoViewModel();
			});
		}

		public static UserAuthManager Auth(this IPrincipalUser principal) => new UserAuthManager(principal);
		public static UserProfileManager Profile(this IPrincipalUser principal) => new UserProfileManager(principal);
		public static UserGroupsManager Group(this IPrincipalUser principal) => new UserGroupsManager(principal);
		public static UserMessagesManager Message(this IPrincipalUser principal) => new UserMessagesManager(principal);
		public static AntiViolenceTimerManager AntiViolence(this IPrincipalUser principal) => new AntiViolenceTimerManager(principal);
	}
}
