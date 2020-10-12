﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Users.DataModels
{
	public class UserWeChat
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserId { get; set; }

		[MaxLength(32), Column(TypeName = "varchar(32)"), Index(IsUnique = true)]
		public string? PrivateId { get; set; }

		[MaxLength(32), Column(TypeName = "varchar(32)"), Index(IsUnique = true)]
		public string? UnionId { get; set; }

		[MaxLength(36)]
		public string NickName { get; set; } = null!;

		public Sex Sex { get; set; }

		[MaxLength(500), Column(TypeName = "varchar(500)")]
		public string HeadImageUrl { get; set; } = null!;

		[MaxLength(24)]
		public string? Province { get; set; }

		[MaxLength(24)]
		public string? City { get; set; }

		[MaxLength(24)]
		public string? Country { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public User? User { get; set; }
		public List<UserWeChatOpenId> OpenIds { get; set; } = new List<UserWeChatOpenId>();
	}
}
