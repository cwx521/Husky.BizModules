using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserMessageManager
	{
		public async Task MarkReadAll() {
			if ( _me.IsAnonymous ) {
				return;
			}

			var sql = $"update UserMessages" +
				$" set   {nameof(UserMessage.IsRead)}=1" +
				$" where {nameof(UserMessage.IsRead)}=0 and {nameof(UserMessage.UserId)}={{0}}";

			await _db.Normalize().Database.ExecuteSqlRawAsync(sql, _me.Id);
		}
	}
}