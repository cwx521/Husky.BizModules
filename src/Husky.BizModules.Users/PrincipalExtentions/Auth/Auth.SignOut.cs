using Husky.Principal.SessionData;

namespace Husky.Principal
{
	public partial class UserAuthManager
	{
		public void SignOut() {
			if ( _me.IsAuthenticated ) {
				_me.AbandonSessionData();
				_me.IdentityManager.DeleteIdentity();
				_me.Id = 0;
			}
		}
	}
}
