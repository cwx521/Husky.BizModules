using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;

namespace Husky.Principal
{
	public partial class UserMessageManager
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