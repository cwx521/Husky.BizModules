#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;

namespace Husky.BizModules.Shopping.DataModels
{
	public interface IShoppingDbContext
	{
		DbContext Normalize();

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductPicture> ProductPictures { get; set; }
		public DbSet<ProductSibling> ProductSiblings { get; set; }
		public DbSet<ProductTag> ProductTags { get; set; }
		public DbSet<ProductTagRelation> ProductTagRelations { get; set; }
		public DbSet<ProductChoiseGroup> ProductChoiseGroups { get; set; }
		public DbSet<ProductChoise> ProductChoises { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<OrderPayment> OrderPayments { get; set; }
		public DbSet<OrderRefund> OrderRefunds { get; set; }
		public DbSet<OrderLog> OrderLogs { get; set; }
		public DbSet<OrderReceiverAddress> OrderReceiverAddresss { get; set; }
	}
}
