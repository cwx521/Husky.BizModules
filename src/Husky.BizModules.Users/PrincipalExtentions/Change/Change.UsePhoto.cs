using System.Threading.Tasks;

namespace Husky.Principal
{
	partial class UserChangeManager
	{
		public async Task<Result> UsePhoto(string photoUrl) {
			if ( _me.IsAnonymous ) {
				return new Failure("需要先登录");
			}

			var user = _db.Users.Find(_me.Id);
			user.PhotoUrl = photoUrl.Left(500);

			await _db.Normalize().SaveChangesAsync();
			return new Success();
		}
	}
}