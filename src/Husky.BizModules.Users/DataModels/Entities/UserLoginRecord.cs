﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Users.DataModels
{
	public class UserLoginRecord
	{
		[Key]
		public int Id { get; set; }

		public int? UserId { get; set; }

		[StringLength(50)]
		public string AttemptedAccount { get; set; } = null!;

		[StringLength(88), Column(TypeName = "varchar(88)")]
		public string? SickPassword { get; set; }

		public LoginResult LoginResult { get; set; }

		[StringLength(500)]
		public string? UserAgent { get; set; }

		[StringLength(39), Column(TypeName = "varchar(39)")]
		public string? Ip { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public User User { get; set; } = null!;
	}
}
