using Husky;
using Husky.BizModules.Users.DataModels;
using Husky.Principal.SessionData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Husky.Principal
{
	public static partial class PrincipalExtensions
	{
		public static UserAuthManager Auth(this IPrincipalUser principal) {
			return new UserAuthManager(principal);
		}

		public static UserChangeManager Change(this IPrincipalUser principal) {
			return new UserChangeManager(principal);
		}

		public static UserGroupManager Group(this IPrincipalUser principal) {
			return new UserGroupManager(principal);
		}

		public static UserMessageManager Message(this IPrincipalUser principal) {
			return new UserMessageManager(principal);
		}

		public static UserQuickView QuickView(this IPrincipalUser principal) {
			var sessionData = principal.SessionData();
			if ( sessionData == null ) {
				return new UserQuickView();
			}
			return (UserQuickView)sessionData.GetOrAdd(nameof(UserQuickView), key => {
				if ( principal.Id == 0 ) {
					principal.Auth().SignOut();
					return new UserQuickView();
				}

				using var scope = principal.ServiceProvider.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<IUsersDbContext>();

				var quickView = db.Users
					.AsNoTracking()
					.Where(x => x.Id == principal.Id)
					.Select(x => new UserQuickView {
						PhotoUrl = x.PhotoUrl ?? (x.WeChat == null ? null : x.WeChat.HeadImageUrl),
						PhoneNumber = x.Phone == null ? null : x.Phone.Number,
						EmailAddress = x.Email == null ? null : x.Email.EmailAddress,
						RegisteredTime = x.RegisteredTime,
						AwaitChangePassword = ActionAwait.NoNeed
					})
					.SingleOrDefault();

				if ( quickView == null ) {
					principal.Auth().SignOut();
				}
				return quickView ?? new UserQuickView();
			});
		}

		public static DateTime AntiViolenceTimer(this IPrincipalUser principal) {
			var sessionData = principal.SessionData();
			if ( sessionData == null ) {
				return DateTime.MinValue;
			}
			return (DateTime)sessionData.GetOrAdd(nameof(AntiViolenceTimer), key => DateTime.Now.AddDays(-1));
		}

		public static void SetAntiViolenceTimer(this IPrincipalUser principal, DateTime? time = null) {
			if ( principal.IsAuthenticated ) {
				var val = time ?? DateTime.Now;
				principal.SessionData()?.AddOrUpdate(nameof(AntiViolenceTimer), val, (key, old) => val);
			}
		}

		public static void ClearAntiViolenceTimer(this IPrincipalUser principal) {
			SetAntiViolenceTimer(principal, DateTime.MinValue);
		}
	}
}
