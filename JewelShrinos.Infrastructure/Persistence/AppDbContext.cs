using JewelShrinos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(x => x.ProductId);
            entity.Property(x => x.Code).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.CostPrice).HasColumnType("decimal(18,2)");
            entity.Property(x => x.SellingPrice).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(x => x.InventoryId);
            entity.HasIndex(x => x.ProductId).IsUnique();
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(x => x.SaleId);
            entity.Property(x => x.SaleNumber).HasMaxLength(50).IsRequired();
            entity.Property(x => x.SubtotalAmount).HasColumnType("decimal(18,2)");
            entity.Property(x => x.TaxAmount).HasColumnType("decimal(18,2)");
            entity.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)");
            entity.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(x => x.SaleDetailId);
            entity.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");
            entity.Property(x => x.LineDiscount).HasColumnType("decimal(18,2)");
        });
    }
}