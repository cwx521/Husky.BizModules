﻿#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Users.DataModels
{
	public static class UsersModelBuilderHelper
	{
		public static void OnUsersModelCreating(this ModelBuilder mb) {

			//PrimaryKeys
			mb.Entity<UserInGroup>().HasKey(x => new { x.UserId, x.GroupId });

			//QueryFilters
			mb.Entity<UserPassword>().HasQueryFilter(x => !x.IsObsoleted);
			mb.Entity<UserMessage>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<UserAddress>().HasQueryFilter(x =>
				x.Status == RowStatus.Active &&
				x.City != null &&
				x.City.Length != 0 &&
				x.ContactName != null &&
				x.ContactName.Length != 0);


			//User
			mb.Entity<User>(user => {
				user.HasOne(x => x.Phone).WithOne(x => x.User).HasForeignKey<UserPhone>(x => x.UserId);
				user.HasOne(x => x.Email).WithOne(x => x.User).HasForeignKey<UserPhone>(x => x.UserId);
				user.HasOne(x => x.WeChat).WithOne(x => x.User).HasForeignKey<UserWeChat>(x => x.UserId);
				user.HasOne(x => x.Real).WithOne(x => x.User).HasForeignKey<UserReal>(x => x.UserId);
				user.HasMany(x => x.Passwords).WithOne(x => x.User).HasForeignKey(x => x.UserId);
				user.HasMany(x => x.InGroups).WithOne().HasForeignKey(x => x.UserId);
				user.HasMany<UserAddress>().WithOne(x => x.User).HasForeignKey(x => x.UserId);
				user.HasMany<UserMessage>().WithOne(x => x.User).HasForeignKey(x => x.UserId);
				user.HasMany<UserLoginRecord>().WithOne(x => x.User).HasForeignKey(x => x.UserId);
			});

			//UserWeChat
			mb.Entity<UserWeChat>(wechat => {
				wechat.HasMany(x => x.OpenIds).WithOne(x => x.WeChat).HasForeignKey(x => x.WeChatId);
			});

			//UserMessage
			mb.Entity<UserMessage>(message => {
				message.HasOne(x => x.PublicContent).WithMany(x => x.UserMessages).HasForeignKey(x => x.PublicContentId);
			});

			//UserGroup
			mb.Entity<UserInGroup>(userInGroup => {
				userInGroup.HasOne(x => x.Group).WithMany().HasForeignKey(x => x.GroupId);
			});
		}
	}
}
