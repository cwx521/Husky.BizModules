using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;
using Husky.TwoFactor;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public partial class UserProfileManager
	{
		public async Task<Result> UsePhone(string newNumber, string verificationCode) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			if ( newNumber.StartsWith('+') ) {
				newNumber = newNumber.Substring(1);
			}
			if ( newNumber.StartsWith("86") ) {
				newNumber = newNumber.Substring(2);
			}

			if ( !newNumber.IsMainlandMobile() ) {
				return new Failure("格式错误");
			}
			if ( _db.UserPhones.Any(x => x.UserId != _me.Id && x.Number == newNumber) ) {
				return new Failure($"{newNumber.Mask()} 已被其它帐号使用");
			}

			var verifyModel = new TwoFactorModel { SendTo = newNumber, Code = verificationCode };
			var verifyResult = await _me.TwoFactor().VerifyTwoFactorCode(verifyModel, true);

			if ( !verifyResult.Ok ) {
				return verifyResult;
			}

			var userPhone = _db.UserPhones.Find(_me.Id);
			if ( userPhone == null ) {
				userPhone = new UserPhone {
					UserId = _me.Id
				};
				_db.UserPhones.Add(userPhone);
			}

			userPhone.Number = newNumber;
			userPhone.IsVerified = true;

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}
