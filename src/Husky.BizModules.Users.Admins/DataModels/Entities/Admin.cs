﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Users.Admins.DataModels
{
	public class Admin
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserId { get; }

		public string DisplayName { get; set; } = null!;

		public bool IsCreator { get; set; }

		public RowStatus Status { get; set; }

		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime CreatedTime { get; set; } = DateTime.Now;


		// nav props

		public List<AdminRole> Roles { get; set; } = new List<AdminRole>();
	}
}