using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public partial class UserMessagesManager
	{
		public async Task<Result> MarkReadAll() {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var sql = $"update UserMessages" +
				$" set   {nameof(UserMessage.IsRead)}=1" +
				$" where {nameof(UserMessage.IsRead)}=0 and {nameof(UserMessage.UserId)}={{0}}";

			await _db.Normalize().Database.ExecuteSqlRawAsync(sql, _me.Id);
			return new Success();
		}
	}
}