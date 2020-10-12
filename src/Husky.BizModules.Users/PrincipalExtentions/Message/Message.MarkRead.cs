using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Husky.Principal
{
	public partial class UserMessageManager
	{
		public async Task MarkRead(params int[] userMessageIdArray) {
			if ( _me.IsAnonymous ) {
				return;
			}

			var rows = _db.UserMessage
				.Where(x => x.UserId == _me.Id)
				.Where(x => userMessageIdArray.Contains(x.Id))
				.Where(x => x.IsRead == false)
				.ToList();

			rows.ForEach(x => x.IsRead = true);
			await _db.Normalize().SaveChangesAsync();
		}
	}
}