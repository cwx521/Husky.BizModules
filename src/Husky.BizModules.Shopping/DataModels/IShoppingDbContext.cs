#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Shopping.DataModels
{
	public interface IShoppingDbContext
	{
		DbContext Normalize();

		public DbSet<Shop> Shops { get; set; }
		public DbSet<ShopPicture> ShopPictures { get; set; }
		public DbSet<ShopLimit> ShopLimits { get; set; }
		public DbSet<ShopProfitWithdrawal> ShopProfitWithdrawals { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductPicture> ProductPictures { get; set; }
		public DbSet<ProductSibling> ProductSiblings { get; set; }
		public DbSet<ProductTag> ProductTags { get; set; }
		public DbSet<ProductTagRelation> ProductTagRelations { get; set; }
		public DbSet<ProductVariationGroup> ProductVariationGroups { get; set; }
		public DbSet<ProductVariation> ProductVariations { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderReceiverAddress> OrderReceiverAddresss { get; set; }
		public DbSet<OrderCartItem> OrderCartItems { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<OrderExpress> OrderExpresses { get; set; }
		public DbSet<OrderPayment> OrderPayments { get; set; }
		public DbSet<OrderRefund> OrderRefunds { get; set; }
		public DbSet<OrderLog> OrderLogs { get; set; }
		public DbSet<OrderComment> OrderComments { get; set; }
		public DbSet<OrderFinalize> OrderFinalizes { get; set; }
	}
}
