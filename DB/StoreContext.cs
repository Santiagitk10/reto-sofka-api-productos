using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        { 
        }

        public DbSet<Product> Products { get; set;}
        public DbSet<Purchase> Purchases { get; set;}
        public DbSet<ProductPurchase> ProductPurchases { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Product>().ToTable("Product");
             modelBuilder.Entity<Purchase>().ToTable("Purchase");

             modelBuilder.Entity<ProductPurchase>()
            .HasKey(pp => new { pp.ProductId, pp.PurchaseId });
             modelBuilder.Entity<ProductPurchase>()
            .HasOne(pp => pp.Product)
            .WithMany(pr => pr.ProductPurchases)
            .HasForeignKey(pp => pp.ProductId);
             modelBuilder.Entity<ProductPurchase>()
            .HasOne(pp => pp.Purchase)
            .WithMany(pu => pu.ProductPurchases)
            .HasForeignKey(pp => pp.PurchaseId);

        }

    }
}