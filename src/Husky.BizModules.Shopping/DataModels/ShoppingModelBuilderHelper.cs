#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
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
			mb.Entity<Order>().HasQueryFilter(x => x.OrderStatus != OrderStatus.Deleted);
			mb.Entity<OrderLog>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductChoiseGroup>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductChoise>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductPicture>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ShoppingCartItem>().HasQueryFilter(x => x.Removed == false);


			//Product
			mb.Entity<Product>(product => {
				product.HasMany(x => x.Pictures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
				product.HasMany(x => x.ChoiseGroups).WithOne().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<ProductChoiseGroup>(choiseGroup => {
				choiseGroup.HasMany(x => x.Choises).WithOne().HasForeignKey(x => x.ChoiseGroupId);
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
				order.HasOne(x => x.Buyer).WithMany().HasForeignKey(x => x.BuyerId);
				order.HasOne(x => x.ReceiverAddress).WithOne(x => x.Order).HasForeignKey<OrderReceiverAddress>(x => x.OrderId);
				order.HasMany(x => x.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
				order.HasMany(x => x.OrderLogs).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
				order.HasMany(x => x.Payments).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
			});
			mb.Entity<OrderItem>(orderItem => {
				orderItem.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<OrderPayment>(orderLog => {
				orderLog.HasMany(x => x.Refunds).WithOne(x => x.SourcePayment).HasForeignKey(x => x.SourcePaymentId);
			});

			//ShoppingCartItem
			mb.Entity<ShoppingCartItem>(shoppingCartItem => {
				shoppingCartItem.HasOne(x => x.Buyer).WithMany().HasForeignKey(x => x.BuyerId);
				shoppingCartItem.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
			});
		}
	}
}
