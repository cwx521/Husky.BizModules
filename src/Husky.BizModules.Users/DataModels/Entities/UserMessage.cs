﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Husky.BizModules.Users.DataModels
{
	public class UserMessage
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		public int? PublicContentId { get; set; }

		[MaxLength(4000)]
		public string? Content { get; set; }

		public bool IsRead { get; set; }

		public RowStatus Status { get; set; } = RowStatus.Active;

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		[JsonIgnore]
		public User User { get; set; } = null!;

		public UserMessagePublicContent? PublicContent { get; set; }
	}
}
