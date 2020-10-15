using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;
using Husky.TwoFactor;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public partial class UserProfileManager
	{
		public async Task<Result> UseNewPassword(string newPassword) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var oldPasswords = _db.UserPasswords.Where(x => !x.IsObsoleted).Where(x => x.UserId == _me.Id).ToList();
			oldPasswords.ForEach(x => x.IsObsoleted = true);

			_db.UserPasswords.Add(new UserPassword {
				UserId = _me.Id,
				Password = Crypto.SHA1(newPassword)
			});

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}

		public async Task<Result> UseNewPasswordWithPhoneValidation(string newPassword, string verificationCode) {
			var userPhone = _db.UserPhones.Find(_me.Id);
			if ( userPhone == null ) {
				return new Failure("需要先绑定手机");
			}

			var verifyModel = new TwoFactorModel { SendTo = userPhone.Number, Code = verificationCode };
			var verifyResult = await _me.TwoFactor().VerifyTwoFactorCode(verifyModel, true);

			if ( !verifyResult.Ok ) {
				return verifyResult;
			}
			userPhone.IsVerified = true;

			return await UseNewPassword(newPassword);
		}
	}
}
