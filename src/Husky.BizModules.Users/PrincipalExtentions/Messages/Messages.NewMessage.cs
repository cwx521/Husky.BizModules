using System.Threading.Tasks;
using Husky.BizModules.Users.DataModels;

namespace Husky.BizModules.Users.PrincipalExtentions
{
	public partial class UserMessagesManager
	{
		public async Task<Result<UserMessage>> NewMessage(string message) {
			if ( _me.IsAnonymous ) {
				return new Failure<UserMessage>("需要先登录");
			}

			var userMessage = new UserMessage {
				UserId = _me.Id,
				Content = message,
			};

			_db.UserMessage.Add(userMessage);
			await _db.Normalize().SaveChangesAsync();

			return new Success<UserMessage>(userMessage);
		}
	}
}