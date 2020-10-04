using Husky.BizModules.Users.DataModels;
using System.Threading.Tasks;

namespace Husky.Principal
{
	partial class UserMessageManager
	{
		public async Task NewMessage(string message) {
			_db.UserMessage.Add(new UserMessage {
				UserId = _me.Id,
				Content = message,
			});
			await _db.Normalize().SaveChangesAsync();
		}
	}
}