﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class ShopPicture
	{
		[Key]
		public int Id { get; set; }

		public int ShopId { get; set; }

		[StringLength(500), Column(TypeName = "varchar(500)"), Required]
		public string PictureUrl { get; set; } = null!;

		[StringLength(50)]
		public string? PictureName { get; set; }

		public MediaType Type { get; set; }

		public RowStatus Status { get; set; }


		// nav props

		public Shop Shop { get; set; } = null!;
	}
}
