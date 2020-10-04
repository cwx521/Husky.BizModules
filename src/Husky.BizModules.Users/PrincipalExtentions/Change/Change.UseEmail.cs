using Husky.BizModules.Users.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace Husky.Principal
{
	partial class UserChangeManager
	{
		public async Task<Result> UseEmail(string newEmailAddress /* , string? verificationCode = null */) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}
			if ( _db.UserEmails.Any(x => x.UserId != _me.Id && x.EmailAddress == newEmailAddress) ) {
				return new Failure($"{newEmailAddress.Mask()} 已被其它帐号使用");
			}

			//if ( verificationCode != null ) {
			//	var validationModel = new TwoFactorModel { SendTo = newEmailAddress, Code = verificationCode };
			//	var validationResult = await _me.TwoFactor().VerifyTwoFactorCode(validationModel, true);

			//	if ( !validationResult.Ok ) {
			//		return validationResult;
			//	}
			//}

			var userEmail = _db.UserEmails.Find(_me.Id);
			if ( userEmail == null ) {
				userEmail = new UserEmail {
					UserId = _me.Id
				};
				_db.UserEmails.Add(userEmail);
			}

			userEmail.EmailAddress = newEmailAddress;
			userEmail.IsVerified = true;

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}
