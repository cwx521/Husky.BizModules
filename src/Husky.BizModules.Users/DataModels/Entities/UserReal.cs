﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Users.DataModels
{
	public class UserReal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserId { get; set; }

		[StringLength(18, MinimumLength = 18), Column(TypeName = "varchar(18)"), Unique]
		public string? SocialIdNumber { get; set; }

		[StringLength(24)]
		public string? RealName { get; set; }

		public Sex? Sex { get; set; }

		public bool IsVerified { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public User User { get; set; } = null!;
	}
}
