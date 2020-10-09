﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Husky.BizModules.Users.DataModels
{
	public class UserWeChatOpenId
	{
		[Key]
		public int Id { get; set; }

		public int WeChatId { get; set; }

		public WeChatOpenIdType OpenIdType { get; set; }

		[MaxLength(32), Column(TypeName = "varchar(32)"), Index(IsUnique = true)]
		public string OpenIdValue { get; set; } = null!;


		// nav props

		[JsonIgnore]
		public UserWeChat WeChat { get; set; } = null!;
	}
}