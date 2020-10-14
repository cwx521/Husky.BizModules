﻿using System;

namespace Husky.BizModules.Users.Admins.PrincipalExtentions
{
	public class AdminInfoViewModel
	{
		public bool IsAdmin { get; set; }
		public string[] Roles { get; set; } = null!;
		public long Powers { get; set; }

		public TEnum MapPowers<TEnum>() where TEnum : Enum => (TEnum)(object)Powers;
	}
}