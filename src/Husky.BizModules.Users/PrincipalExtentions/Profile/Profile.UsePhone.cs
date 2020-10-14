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
			if ( _db.UserPhones.Any(x => x.UserId != _me.Id && x.Number == newNumber) ) {
				return new Failure($"{newNumber.Mask()} 已被其它帐号使用");
			}

			var validationModel = new TwoFactorModel { SendTo = newNumber, Code = verificationCode };
			var validationResult = await _me.TwoFactor().VerifyTwoFactorCode(validationModel, true);

			if ( !validationResult.Ok ) {
				return validationResult;
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
