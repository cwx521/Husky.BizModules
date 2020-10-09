using Husky.BizModules.Users.DataModels;
using System.Threading.Tasks;

namespace Husky.Principal
{
	partial class UserProfileManager
	{
		public async Task<Result> UseReal(string socialIdNumber, string realName, bool isVerified = false) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}
			if ( socialIdNumber.IsMainlandSocialNumber() ) {
				return new Failure("身份证号码无效");
			}

			var userReal = _db.UserReals.Find(_me.Id);
			if ( userReal == null ) {
				userReal = new UserReal();
				_db.UserReals.Add(userReal);
			}

			userReal.SocialIdNumber = socialIdNumber;
			userReal.RealName = realName;
			userReal.Sex = StringTest.GetSexFromMainlandSocialNumber(socialIdNumber);
			userReal.IsVerified = isVerified;

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}