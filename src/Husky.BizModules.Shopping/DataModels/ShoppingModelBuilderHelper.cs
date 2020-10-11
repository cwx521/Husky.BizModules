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
			mb.Entity<Order>().HasQueryFilter(x => x.Status != OrderStatus.Deleted);
			mb.Entity<OrderLog>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<OrderExpress>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<OrderComment>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductVariationGroup>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductVariation>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ProductPicture>().HasQueryFilter(x => x.Status == RowStatus.Active);
			mb.Entity<ShoppingCartItem>().HasQueryFilter(x => x.Removed == false);


			//Product
			mb.Entity<Product>(product => {
				product.HasMany(x => x.Pictures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
				product.HasMany(x => x.VariationGroups).WithOne().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<ProductVariationGroup>(variationGroup => {
				variationGroup.HasMany(x => x.Variations).WithOne().HasForeignKey(x => x.GroupId);
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
			mb.Entity<OrderItem>(orderItem => {
				orderItem.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
			});
			mb.Entity<OrderPayment>(orderLog => {
				orderLog.HasMany(x => x.Refunds).WithOne(x => x.SourcePayment).HasForeignKey(x => x.SourcePaymentId);
			});

			//ShoppingCartItem
			mb.Entity<ShoppingCartItem>(shoppingCartItem => {
				shoppingCartItem.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
			});

			//Withdrawal
			mb.Entity<Withdrawal>(withdrawal => {
				withdrawal.HasMany(x => x.AssosicatedOrderFinalizes).WithOne(x => x.Withdrawal).HasForeignKey(x => x.WithdrawalId);
			});
		}
	}
}
