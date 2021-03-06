﻿using System.Linq;
using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;
using Husky.Principal;
using Husky.TwoFactor;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public partial class UserAuthManager
	{
		public async Task<Result> SignInWithPhone(string mobileNumber, string verificationCode) {
			if ( string.IsNullOrEmpty(mobileNumber) || string.IsNullOrEmpty(verificationCode) ) {
				return new Failure(LoginResult.InvalidInput.ToLabel());
			}
			if ( !mobileNumber.IsMainlandMobile() ) {
				return new Failure(LoginResult.InvalidInput.ToLabel());
			}

			//读取用户记录
			var user = _db.Users.SingleOrDefault(x => x.Phone != null && x.Phone.Number == mobileNumber);

			var verifyModel = new TwoFactorModel { SendTo = mobileNumber, Code = verificationCode };
			var verifyResult = await _me.TwoFactor().VerifyTwoFactorCode(verifyModel, true);

			//验证码不通过
			if ( !verifyResult.Ok ) {
				return await AddLoginRecord(LoginResult.ErrorTwoFactorCode, mobileNumber, user?.Id);
			}

			//如果通过手机号没找到已注册用户，判断用户当前是否已经通过其它方式登录，是的话直接使用该用户身份
			if ( user == null && _me.IsAuthenticated ) {
				user = _db.Users
					.Where(x => x.Phone == null)
					.Where(x => x.Id == _me.Id)
					.SingleOrDefault();
			}

			//如果仍然没找到，自动注册新建用户
			if ( user == null ) {
				user = new User {
					Phone = new UserPhone {
						Number = mobileNumber,
						IsVerified = true
					}
				};
				_db.Users.Add(user);
			}
			else {
				//用户记录是异常状态时，阻止获得登录身份
				if ( user.Status == RowStatus.Suspended ) {
					return await AddLoginRecord(LoginResult.RejectedAccountSuspended, mobileNumber, user.Id);
				}
				if ( user.Status == RowStatus.DeletedByAdmin || user.Status == RowStatus.DeletedByUser ) {
					return await AddLoginRecord(LoginResult.RejectedAccountDeleted, mobileNumber, user.Id);
				}
				if ( user.Status != RowStatus.Active ) {
					return await AddLoginRecord(LoginResult.RejectedAccountInactive, mobileNumber, user.Id);
				}
			}

			await _db.Normalize().SaveChangesAsync();

			_me.Id = user.Id;
			_me.DisplayName = user.DisplayName ?? mobileNumber.Mask()!;
			_me.IdentityManager.SaveIdentity(_me);
			_me.User().IsTwoFactorValidated = true;

			return await AddLoginRecord(LoginResult.Success, mobileNumber, user.Id);
		}
	}
}
