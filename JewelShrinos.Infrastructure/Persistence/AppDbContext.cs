using JewelShrinos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();
    public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");
            entity.HasKey(x => x.CategoryId);

            entity.Property(x => x.CategoryId).HasColumnName("category_id");
            entity.Property(x => x.Name).HasColumnName("name");
            entity.Property(x => x.Description).HasColumnName("description");
            entity.Property(x => x.Status).HasColumnName("status");
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("material");
            entity.HasKey(x => x.MaterialId);

            entity.Property(x => x.MaterialId).HasColumnName("material_id");
            entity.Property(x => x.Name).HasColumnName("name");
            entity.Property(x => x.Type).HasColumnName("type");
            entity.Property(x => x.Purity).HasColumnName("purity");
            entity.Property(x => x.ReferencePrice).HasColumnName("reference_price");
            entity.Property(x => x.Status).HasColumnName("status");
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("supplier");
            entity.HasKey(x => x.SupplierId);

            entity.Property(x => x.SupplierId).HasColumnName("supplier_id");
            entity.Property(x => x.Name).HasColumnName("name");
            entity.Property(x => x.RucDni).HasColumnName("ruc_dni");
            entity.Property(x => x.ContactName).HasColumnName("contact_name");
            entity.Property(x => x.Email).HasColumnName("email");
            entity.Property(x => x.Phone).HasColumnName("phone");
            entity.Property(x => x.Address).HasColumnName("address");
            entity.Property(x => x.Status).HasColumnName("status");
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });

       modelBuilder.Entity<Product>(entity =>
{
    entity.ToTable("products");
    entity.HasKey(x => x.ProductId);

    entity.Property(x => x.ProductId).HasColumnName("product_id");
    entity.Property(x => x.Code).HasColumnName("code");
    entity.Property(x => x.Barcode).HasColumnName("barcode");
    entity.Property(x => x.QrCode).HasColumnName("qr_code");
    entity.Property(x => x.Name).HasColumnName("name");
    entity.Property(x => x.Description).HasColumnName("description");
    entity.Property(x => x.CategoryId).HasColumnName("category_id");
    entity.Property(x => x.MaterialId).HasColumnName("material_id");
    entity.Property(x => x.SupplierId).HasColumnName("supplier_id");
    entity.Property(x => x.CostPrice).HasColumnName("cost_price").HasColumnType("numeric(18,2)");
    entity.Property(x => x.SellingPrice).HasColumnName("selling_price").HasColumnType("numeric(18,2)");
    entity.Property(x => x.Weight).HasColumnName("weight").HasColumnType("numeric(18,2)");
    entity.Property(x => x.Certificate).HasColumnName("certificate");
    entity.Property(x => x.ImageUrl).HasColumnName("image_url");
    entity.Property(x => x.Sku).HasColumnName("sku");
    entity.Property(x => x.Status).HasColumnName("status");
    entity.Property(x => x.CreatedAt).HasColumnName("created_at");
    entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

    entity.HasOne(x => x.Category)
          .WithMany(c => c.Products)
          .HasForeignKey(x => x.CategoryId)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(x => x.Material)
          .WithMany(m => m.Products)
          .HasForeignKey(x => x.MaterialId)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(x => x.Supplier)
          .WithMany(s => s.Products)
          .HasForeignKey(x => x.SupplierId)
          .OnDelete(DeleteBehavior.Restrict);
});

modelBuilder.Entity<InventoryMovement>(entity =>
{
    entity.ToTable("inventory_movement");
    entity.HasKey(x => x.InventoryMovementId);

    entity.Property(x => x.InventoryMovementId).HasColumnName("inventory_movement_id");
    entity.Property(x => x.ProductId).HasColumnName("product_id");
    entity.Property(x => x.InventoryId).HasColumnName("inventory_id");
    entity.Property(x => x.MovementType).HasColumnName("movement_type").HasMaxLength(50).IsRequired();

    entity.HasOne(x => x.Product)
          .WithMany(p => p.InventoryMovements)
          .HasForeignKey(x => x.ProductId)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(x => x.Inventory)
          .WithMany(i => i.InventoryMovements)
          .HasForeignKey(x => x.InventoryId)
          .OnDelete(DeleteBehavior.Restrict);
});
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("inventory");
            entity.HasKey(x => x.InventoryId);

            entity.Property(x => x.InventoryId).HasColumnName("inventory_id");
            entity.Property(x => x.ProductId).HasColumnName("product_id");

            entity.HasIndex(x => x.ProductId).IsUnique();
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("sales");
            entity.HasKey(x => x.SaleId);

            entity.Property(x => x.SaleId).HasColumnName("sale_id");
            entity.Property(x => x.SaleNumber).HasColumnName("sale_number").HasMaxLength(50).IsRequired();
            entity.Property(x => x.SubtotalAmount).HasColumnName("subtotal_amount").HasColumnType("numeric(18,2)");
            entity.Property(x => x.TaxAmount).HasColumnName("tax_amount").HasColumnType("numeric(18,2)");
            entity.Property(x => x.DiscountAmount).HasColumnName("discount_amount").HasColumnType("numeric(18,2)");
            entity.Property(x => x.TotalAmount).HasColumnName("total_amount").HasColumnType("numeric(18,2)");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.ToTable("sale_details");
            entity.HasKey(x => x.SaleDetailId);

            entity.Property(x => x.SaleDetailId).HasColumnName("sale_detail_id");
            entity.Property(x => x.UnitPrice).HasColumnName("unit_price").HasColumnType("numeric(18,2)");
            entity.Property(x => x.Subtotal).HasColumnName("subtotal").HasColumnType("numeric(18,2)");
            entity.Property(x => x.LineDiscount).HasColumnName("line_discount").HasColumnType("numeric(18,2)");
        });
     
    }
}