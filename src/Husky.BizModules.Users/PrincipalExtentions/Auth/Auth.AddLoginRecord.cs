using Husky.BizModules.Users.DataModels;
using System.Threading.Tasks;

namespace Husky.Principal
{
	partial class UserAuthManager
	{
		public async Task<LoginResult> AddLoginRecord(LoginResult result, string inputAccount, int? knownUserId = null, string? sickPassword = null) {
			var ip = _http.Connection.RemoteIpAddress;
			var ipString = ip.MapToIPv4().ToString();

			var encryptedSickPassword = string.IsNullOrEmpty(sickPassword) || inputAccount.Length == 0
				? sickPassword
				: sickPassword.Length > 25
					? sickPassword.Left(88)
					: Crypto.Encrypt(sickPassword, iv: inputAccount);

			_db.UserLoginRecords.Add(new UserLoginRecord {
				LoginResult = result,
				UserId = knownUserId,
				AttemptedAccount = inputAccount ?? "",
				SickPassword = encryptedSickPassword,
				UserAgent = _http.Request.UserAgent(),
				Ip = ipString
			});

			await _db.Normalize().SaveChangesAsync();

			if ( result != LoginResult.Success ) {
				SignOut();
			}
			return result;
		}
	}
}
