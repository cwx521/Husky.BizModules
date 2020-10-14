using System;
using Husky.Principal;
using Husky.Principal.SessionData;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public sealed partial class AntiViolenceTimerManager
	{
		internal AntiViolenceTimerManager(IPrincipalUser principal) {
			_me = principal;
		}

		private const string _timerKey = "AntiViolenceTimer";
		private readonly IPrincipalUser _me;

		public const string ViewName = "_AntiViolence";

		public DateTime GetTimer() {
			var sessionData = _me.SessionData();
			if ( sessionData == null ) {
				return DateTime.MinValue;
			}
			return (DateTime)sessionData.GetOrAdd(_timerKey, key => DateTime.Now.AddDays(-1));
		}

		public void SetTimer(DateTime? time = null) {
			if ( _me.IsAuthenticated ) {
				var val = time ?? DateTime.Now;
				_me.SessionData()?.AddOrUpdate(_timerKey, val, (key, old) => val);
			}
		}

		public void ClearTimer() {
			SetTimer(DateTime.MinValue);
		}
	}
}
