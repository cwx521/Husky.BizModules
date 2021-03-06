﻿#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Shopping.DataModels
{
	public static class ShoppingModelBuilderHelper
	{
		public static void OnUsersModelCreating(this ModelBuilder mb) {

			//Keys
			mb.Entity<ProductSibling>().HasKey(x => new { x.ProductId, x.SiblingProductId });
			mb.Entity<ProductTagRelation>().HasKey(x => new { x.ProductId, x.ProductTagId });

			//Filters
			mb.Entity<Shop>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<Order>().HasQueryFilter(x => x.Status != OrderStatus.Deleted);
			mb.Entity<OrderLog>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<OrderExpress>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<OrderComment>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductVariationGroup>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductVariation>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductPicture>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<OrderCartItem>().HasQueryFilter(x => x.Deleted == false);


			//Shop
			mb.Entity<Shop>(shop => {
				shop.HasOne(x => x.Limit).WithOne(x => x.Shop).HasForeignKey<ShopLimit>(x => x.ShopId);
				shop.HasMany<ShopPicture>().WithOne(x => x.Shop).HasForeignKey(x => x.ShopId);
				shop.HasMany<Product>().WithOne(x => x.Shop).HasForeignKey(x => x.ShopId);
				shop.HasMany<ShopProfitWithdrawal>().WithOne(x => x.Shop).HasForeignKey(x => x.ShopId).OnDelete(DeleteBehavior.Restrict);
			});
			mb.Entity<ShopProfitWithdrawal>(withdrawal => {
				withdrawal.HasMany(x => x.AssosicatedOrderFinalizes).WithOne(x => x.Withdrawal).HasForeignKey(x => x.WithdrawalId);
			});

			//Product
			mb.Entity<Product>(product => {
				product.HasMany(x => x.Pictures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
				product.HasMany(x => x.VariationGroups).WithOne().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<ProductVariation>(variation => {
				variation.HasOne(x => x.Group).WithMany(x => x.Variations).HasForeignKey(x => x.GroupId);
			});
			mb.Entity<ProductTagRelation>(tagRelations => {
				tagRelations.HasOne(x => x.Product).WithMany(x => x.TagRelations).HasForeignKey(x => x.ProductId);
				tagRelations.HasOne(x => x.ProductTag).WithMany(x => x.ProductRelations).HasForeignKey(x => x.ProductTagId);
			});
			mb.Entity<ProductSibling>(sibling => {
				sibling.HasOne(x => x.Product).WithMany(x => x.Siblings).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
				sibling.HasOne(x => x.SiblingProduct).WithMany().HasForeignKey(x => x.SiblingProductId).OnDelete(DeleteBehavior.Restrict);
			});

			//Order
			mb.Entity<Order>(order => {
				order.HasOne(x => x.ReceiverAddress).WithOne(x => x.Order).HasForeignKey<OrderReceiverAddress>(x => x.OrderId);
				order.HasOne(x => x.Finalize).WithOne(x => x.Order).HasForeignKey<OrderReceiverAddress>(x => x.OrderId);
				order.HasMany(x => x.Items).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
				order.HasMany(x => x.Expresses).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
				order.HasMany(x => x.Logs).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
				order.HasMany(x => x.Payments).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
				order.HasMany(x => x.Comments).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
			});
			mb.Entity<OrderCartItem>(cartItem => {
				cartItem.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<OrderItem>(orderItem => {
				orderItem.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<OrderPayment>(orderLog => {
				orderLog.HasMany(x => x.Refunds).WithOne(x => x.OriginalPayment).HasForeignKey(x => x.OriginalPaymentId);
			});

		}
	}
}
