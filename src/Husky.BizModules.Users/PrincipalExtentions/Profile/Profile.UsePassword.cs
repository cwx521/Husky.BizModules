﻿using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;
using Husky.TwoFactor;

namespace Husky.Principal
{
	public partial class UserProfileManager
	{
		public async Task<Result> UsePassword(string newPassword, string verificationCode) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var userPhone = _db.UserPhones.Find(_me.Id);
			if ( userPhone == null ) {
				return new Failure("需要先绑定手机才能设置密码");
			}

			var validationModel = new TwoFactorModel { SendTo = userPhone.Number, Code = verificationCode };
			var validationResult = await _me.TwoFactor().VerifyTwoFactorCode(validationModel, true);

			if ( !validationResult.Ok ) {
				return validationResult;
			}
			userPhone.IsVerified = true;

			var oldPasswords = _db.UserPasswords.Where(x => !x.IsObsoleted).Where(x => x.UserId == _me.Id).ToList();
			oldPasswords.ForEach(x => x.IsObsoleted = true);

			_db.UserPasswords.Add(new UserPassword {
				UserId = _me.Id,
				Password = Crypto.SHA1(newPassword)
			});

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}
