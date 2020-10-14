using System;
using System.Collections.Generic;

namespace Husky.BizModules.Users.Admins.DataModels
{
	public class AdminRole
	{
		public int Id { get; set; }

		public string RoleName { get; set; } = null!;

		public long Powers { get; set; }


		// nav props

		public List<Admin> Admins { get; set; } = new List<Admin>();


		// calculation

		public T EnumPowers<T>() where T : Enum => (T)(object)Powers;
	}
}
