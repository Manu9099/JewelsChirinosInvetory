
using JewelShrinos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();

    // Descomenta estos si YA tienes estas clases en Core
    // public DbSet<Customer> Customers => Set<Customer>();
    // public DbSet<Return> Returns => Set<Return>();
    // public DbSet<ReturnDetail> ReturnDetails => Set<ReturnDetail>();
    // public DbSet<User> Users => Set<User>();
    // public DbSet<SunatIntegration> SunatIntegrations => Set<SunatIntegration>();
    // public DbSet<Configuration> Configurations => Set<Configuration>();
    // public DbSet<Audit> AuditLogs => Set<Audit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(x => x.CategoryId);

            entity.Property(x => x.CategoryId).HasColumnName("category_id");
            entity.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(x => x.Description).HasColumnName("description").HasMaxLength(500);
            entity.Property(x => x.Status).HasColumnName("status").IsRequired();
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("materials");
            entity.HasKey(x => x.MaterialId);

            entity.Property(x => x.MaterialId).HasColumnName("material_id");
            entity.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(x => x.Type).HasColumnName("type").HasMaxLength(50).IsRequired();
            entity.Property(x => x.Purity).HasColumnName("purity").HasMaxLength(50);
            entity.Property(x => x.ReferencePrice).HasColumnName("reference_price").HasColumnType("numeric(12,2)");
            entity.Property(x => x.Status).HasColumnName("status").IsRequired();
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(x => x.Type).HasDatabaseName("idx_materials_type");
            entity.HasIndex(x => x.Purity).HasDatabaseName("idx_materials_purity");
            entity.HasIndex(x => x.Status).HasDatabaseName("idx_materials_status");
            entity.HasIndex(x => new { x.Type, x.Purity }).IsUnique().HasDatabaseName("unique_material_type_purity");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("suppliers");
            entity.HasKey(x => x.SupplierId);

            entity.Property(x => x.SupplierId).HasColumnName("supplier_id");
            entity.Property(x => x.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            entity.Property(x => x.RucDni).HasColumnName("ruc_dni").HasMaxLength(20);
            entity.Property(x => x.ContactName).HasColumnName("contact_name").HasMaxLength(100);
            entity.Property(x => x.Email).HasColumnName("email").HasMaxLength(100);
            entity.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(20);
            entity.Property(x => x.Address).HasColumnName("address").HasMaxLength(300);
            entity.Property(x => x.Status).HasColumnName("status").IsRequired();
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(x => x.RucDni).IsUnique();
            entity.HasIndex(x => x.Name).HasDatabaseName("idx_suppliers_name");
            entity.HasIndex(x => x.Status).HasDatabaseName("idx_suppliers_status");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(x => x.ProductId);

            entity.Property(x => x.ProductId).HasColumnName("product_id");
            entity.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            entity.Property(x => x.Barcode).HasColumnName("barcode").HasMaxLength(50);
            entity.Property(x => x.QrCode).HasColumnName("qr_code").HasMaxLength(500);
            entity.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(x => x.Description).HasColumnName("description").HasMaxLength(1000);
            entity.Property(x => x.CategoryId).HasColumnName("category_id");
            entity.Property(x => x.MaterialId).HasColumnName("material_id");
            entity.Property(x => x.SupplierId).HasColumnName("supplier_id");
            entity.Property(x => x.CostPrice).HasColumnName("cost_price").HasColumnType("numeric(12,2)");
            entity.Property(x => x.SellingPrice).HasColumnName("selling_price").HasColumnType("numeric(12,2)");
            entity.Property(x => x.Weight).HasColumnName("weight").HasColumnType("numeric(10,3)");
            entity.Property(x => x.Certificate).HasColumnName("certificate").HasMaxLength(100);
            entity.Property(x => x.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
            entity.Property(x => x.Sku).HasColumnName("sku").HasMaxLength(100);
            entity.Property(x => x.Status).HasColumnName("status").IsRequired();
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(x => x.Code).IsUnique();
            entity.HasIndex(x => x.Barcode).IsUnique();
            entity.HasIndex(x => x.QrCode).IsUnique();

            entity.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId)
                .HasConstraintName("fk_products_categories")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Material)
                .WithMany(m => m.Products)
                .HasForeignKey(x => x.MaterialId)
                .HasConstraintName("fk_products_materials")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(x => x.SupplierId)
                .HasConstraintName("fk_products_suppliers")
                .OnDelete(DeleteBehavior.Restrict);
        });
modelBuilder.Entity<InventoryMovement>(entity =>
{
    entity.ToTable("inventory_movements");
    entity.HasKey(x => x.InventoryMovementId);

    // usa aquí el nombre REAL de tu PK en BD:
    entity.Property(x => x.InventoryMovementId).HasColumnName("movement_id");

    entity.Property(x => x.ProductId).HasColumnName("product_id");
   
    entity.Property(x => x.UserId).HasColumnName("user_id");

    entity.Property(x => x.MovementType).HasColumnName("movement_type").HasMaxLength(50).IsRequired();
    entity.Property(x => x.Quantity).HasColumnName("quantity");
    entity.Property(x => x.StockBefore).HasColumnName("stock_before");
    entity.Property(x => x.StockAfter).HasColumnName("stock_after");
    entity.Property(x => x.ReferenceType).HasColumnName("reference_type").HasMaxLength(50);
    entity.Property(x => x.ReferenceId).HasColumnName("reference_id");
    entity.Property(x => x.Observations).HasColumnName("observations").HasMaxLength(500);
    entity.Property(x => x.MovementDate).HasColumnName("movement_date");

  entity.HasOne(x => x.Product)
        .WithMany(p => p.InventoryMovements)
        .HasForeignKey(x => x.ProductId)
        .OnDelete(DeleteBehavior.Restrict);
});

modelBuilder.Entity<Inventory>(entity =>
{
    entity.ToTable("inventory");
    entity.HasKey(x => x.InventoryId);

    entity.Property(x => x.InventoryId).HasColumnName("inventory_id");
    entity.Property(x => x.ProductId).HasColumnName("product_id");
    entity.Property(x => x.AvailableStock).HasColumnName("available_stock");
    entity.Property(x => x.ReservedStock).HasColumnName("reserved_stock");
    entity.Property(x => x.SoldStock).HasColumnName("sold_stock");
    entity.Property(x => x.DamagedStock).HasColumnName("damaged_stock");
    entity.Property(x => x.LastCostPrice).HasColumnName("last_cost_price").HasColumnType("numeric(12,2)");
    entity.Property(x => x.LastSellingPrice).HasColumnName("last_selling_price").HasColumnType("numeric(12,2)");
    entity.Property(x => x.LastPurchaseDate).HasColumnName("last_purchase_date");
    entity.Property(x => x.LastSaleDate).HasColumnName("last_sale_date");
    entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

    entity.HasIndex(x => x.ProductId).IsUnique();

        entity.Ignore("InventoryMovements");


    entity.HasOne(x => x.Product)
        .WithOne(p => p.Inventory)
        .HasForeignKey<Inventory>(x => x.ProductId)
        .HasConstraintName("fk_inventory_products")
        .OnDelete(DeleteBehavior.Cascade);
});

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.ToTable("purchases");
            entity.HasKey(x => x.PurchaseId);

            entity.Property(x => x.PurchaseId).HasColumnName("purchase_id");
            entity.Property(x => x.PurchaseNumber).HasColumnName("purchase_number").HasMaxLength(50).IsRequired();
            entity.Property(x => x.SupplierId).HasColumnName("supplier_id");
            entity.Property(x => x.PurchaseDate).HasColumnName("purchase_date").HasColumnType("date");
            entity.Property(x => x.DeliveryDate).HasColumnName("delivery_date").HasColumnType("date");
            entity.Property(x => x.TotalAmount).HasColumnName("total_amount").HasColumnType("numeric(12,2)");
            entity.Property(x => x.PurchaseStatus).HasColumnName("purchase_status").HasMaxLength(50).IsRequired();
            entity.Property(x => x.Observations).HasColumnName("observations").HasMaxLength(1000);
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(100);
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(x => x.PurchaseNumber).IsUnique();

                    entity.HasOne(x => x.Supplier)
                    .WithMany(s => s.Purchases)
                    .HasForeignKey(x => x.SupplierId)
                    .HasConstraintName("fk_purchases_suppliers")
                    .OnDelete(DeleteBehavior.Restrict);
        });

  modelBuilder.Entity<PurchaseDetail>(entity =>
{
    entity.ToTable("purchase_details");
    entity.HasKey(x => x.PurchaseDetailId);

    entity.Property(x => x.PurchaseDetailId).HasColumnName("purchase_detail_id");
    entity.Property(x => x.PurchaseId).HasColumnName("purchase_id");
    entity.Property(x => x.ProductId).HasColumnName("product_id");
    entity.Property(x => x.Quantity).HasColumnName("quantity");
    entity.Property(x => x.UnitPrice).HasColumnName("unit_price").HasColumnType("numeric(12,2)");
    entity.Property(x => x.Subtotal).HasColumnName("subtotal").HasColumnType("numeric(12,2)");
    entity.Property(x => x.QuantityReceived).HasColumnName("quantity_received");
    entity.Property(x => x.Observations).HasColumnName("observations").HasMaxLength(500);
    entity.Property(x => x.CreatedAt).HasColumnName("created_at");

    entity.HasOne(x => x.Purchase)
        .WithMany(p => p.PurchaseDetails)
        .HasForeignKey(x => x.PurchaseId)
        .HasConstraintName("fk_purchase_details_purchases")
        .OnDelete(DeleteBehavior.Cascade);

    entity.HasOne(x => x.Product)
        .WithMany(p => p.PurchaseDetails)
        .HasForeignKey(x => x.ProductId)
        .HasConstraintName("fk_purchase_details_products")
        .OnDelete(DeleteBehavior.Restrict);
});

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("sales");
            entity.HasKey(x => x.SaleId);

            entity.Property(x => x.SaleId).HasColumnName("sale_id");
            entity.Property(x => x.SaleNumber).HasColumnName("sale_number").HasMaxLength(50).IsRequired();
            entity.Property(x => x.CustomerId).HasColumnName("customer_id");
            entity.Property(x => x.SubtotalAmount).HasColumnName("subtotal_amount").HasColumnType("numeric(12,2)");
            entity.Property(x => x.TaxAmount).HasColumnName("tax_amount").HasColumnType("numeric(12,2)");
            entity.Property(x => x.DiscountAmount).HasColumnName("discount_amount").HasColumnType("numeric(12,2)");
            entity.Property(x => x.TotalAmount).HasColumnName("total_amount").HasColumnType("numeric(12,2)");
            entity.Property(x => x.PaymentMethod).HasColumnName("payment_method").HasMaxLength(50);
            entity.Property(x => x.SaleStatus).HasColumnName("sale_status").HasMaxLength(50).IsRequired();
            entity.Property(x => x.InvoiceNumber).HasColumnName("invoice_number").HasMaxLength(50);
            entity.Property(x => x.InvoiceUrl).HasColumnName("invoice_url").HasMaxLength(500);
            entity.Property(x => x.SunatResponseCode).HasColumnName("sunat_response_code").HasMaxLength(50);
            entity.Property(x => x.SunatTicketNumber).HasColumnName("sunat_ticket_number").HasMaxLength(100);
            entity.Property(x => x.Observations).HasColumnName("observations").HasMaxLength(500);
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(100);
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(x => x.SaleNumber).IsUnique();
            
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.ToTable("sale_details");
            entity.HasKey(x => x.SaleDetailId);

            entity.Property(x => x.SaleDetailId).HasColumnName("sale_detail_id");
            entity.Property(x => x.SaleId).HasColumnName("sale_id");
            entity.Property(x => x.ProductId).HasColumnName("product_id");
            entity.Property(x => x.Quantity).HasColumnName("quantity");
            entity.Property(x => x.UnitPrice).HasColumnName("unit_price").HasColumnType("numeric(12,2)");
            entity.Property(x => x.Subtotal).HasColumnName("subtotal").HasColumnType("numeric(12,2)");
            entity.Property(x => x.LineDiscount).HasColumnName("line_discount").HasColumnType("numeric(12,2)");
            entity.Property(x => x.DetailStatus).HasColumnName("detail_status").HasMaxLength(50);
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");

            entity.HasOne(x => x.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(x => x.SaleId)
                .HasConstraintName("fk_sale_details_sales")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("fk_sale_details_products")
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- OPCIONALES: solo si esas clases ya existen ----

        // modelBuilder.Entity<Customer>(entity =>
        // {
        //     entity.ToTable("customers");
        //     entity.HasKey(x => x.CustomerId);
        // });

        // modelBuilder.Entity<Return>(entity =>
        // {
        //     entity.ToTable("returns");
        //     entity.HasKey(x => x.ReturnId);
        // });

        // modelBuilder.Entity<ReturnDetail>(entity =>
        // {
        //     entity.ToTable("return_details");
        //     entity.HasKey(x => x.ReturnDetailId);
        // });

        // modelBuilder.Entity<User>(entity =>
        // {
        //     entity.ToTable("users");
        //     entity.HasKey(x => x.UserId);
        // });

        // modelBuilder.Entity<SunatIntegration>(entity =>
        // {
        //     entity.ToTable("sunat_integration");
        //     entity.HasKey(x => x.SunatId);
        // });

        // modelBuilder.Entity<Configuration>(entity =>
        // {
        //     entity.ToTable("configuration");
        //     entity.HasKey(x => x.ConfigId);
        // });

        // modelBuilder.Entity<Audit>(entity =>
        // {
        //     entity.ToTable("audit");
        //     entity.HasKey(x => x.AuditId);
        // });
    }
}

