using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelShrinos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSnakeCaseMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovements_Inventories_InventoryId",
                table: "InventoryMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovements_Products_ProductId",
                table: "InventoryMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovements_Products_ProductId1",
                table: "InventoryMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Material_MaterialId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplier_SupplierId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Supplier_SupplierId",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Products_ProductId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Return_Sales_SaleId",
                table: "Return");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnDetail_SaleDetails_SaleDetailId",
                table: "ReturnDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customer_CustomerId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryMovements",
                table: "InventoryMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "supplier");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "sales");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "material");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "category");

            migrationBuilder.RenameTable(
                name: "SaleDetails",
                newName: "sale_details");

            migrationBuilder.RenameTable(
                name: "InventoryMovements",
                newName: "inventory_movement");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "inventory");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "supplier",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "supplier",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "supplier",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "supplier",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "supplier",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "supplier",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RucDni",
                table: "supplier",
                newName: "ruc_dni");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "supplier",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "supplier",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "supplier",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "sales",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "sales",
                newName: "tax_amount");

            migrationBuilder.RenameColumn(
                name: "SubtotalAmount",
                table: "sales",
                newName: "subtotal_amount");

            migrationBuilder.RenameColumn(
                name: "SaleNumber",
                table: "sales",
                newName: "sale_number");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "sales",
                newName: "discount_amount");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "sales",
                newName: "sale_id");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CustomerId",
                table: "sales",
                newName: "IX_sales_CustomerId");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "products",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "products",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Sku",
                table: "products",
                newName: "sku");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "products",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Certificate",
                table: "products",
                newName: "certificate");

            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "products",
                newName: "barcode");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "products",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "products",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "products",
                newName: "selling_price");

            migrationBuilder.RenameColumn(
                name: "QrCode",
                table: "products",
                newName: "qr_code");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "products",
                newName: "material_id");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "products",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "products",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CostPrice",
                table: "products",
                newName: "cost_price");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "products",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "products",
                newName: "IX_products_supplier_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_MaterialId",
                table: "products",
                newName: "IX_products_material_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "material",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "material",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Purity",
                table: "material",
                newName: "purity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "material",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "material",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ReferencePrice",
                table: "material",
                newName: "reference_price");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "material",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "material",
                newName: "material_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "category",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "category",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "category",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "category",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "category",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "category",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "sale_details",
                newName: "subtotal");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "sale_details",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "LineDiscount",
                table: "sale_details",
                newName: "line_discount");

            migrationBuilder.RenameColumn(
                name: "SaleDetailId",
                table: "sale_details",
                newName: "sale_detail_id");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_SaleId",
                table: "sale_details",
                newName: "IX_sale_details_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_ProductId",
                table: "sale_details",
                newName: "IX_sale_details_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "inventory_movement",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "MovementType",
                table: "inventory_movement",
                newName: "movement_type");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "inventory_movement",
                newName: "inventory_id");

            migrationBuilder.RenameColumn(
                name: "InventoryMovementId",
                table: "inventory_movement",
                newName: "inventory_movement_id");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryMovements_ProductId1",
                table: "inventory_movement",
                newName: "IX_inventory_movement_ProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryMovements_ProductId",
                table: "inventory_movement",
                newName: "IX_inventory_movement_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryMovements_InventoryId",
                table: "inventory_movement",
                newName: "IX_inventory_movement_inventory_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "inventory",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "inventory",
                newName: "inventory_id");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_ProductId",
                table: "inventory",
                newName: "IX_inventory_product_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "products",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId1",
                table: "products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId1",
                table: "products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_supplier",
                table: "supplier",
                column: "supplier_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sales",
                table: "sales",
                column: "sale_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_material",
                table: "material",
                column: "material_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sale_details",
                table: "sale_details",
                column: "sale_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_movement",
                table: "inventory_movement",
                column: "inventory_movement_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory",
                table: "inventory",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId1",
                table: "products",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_products_MaterialId1",
                table: "products",
                column: "MaterialId1");

            migrationBuilder.CreateIndex(
                name: "IX_products_SupplierId1",
                table: "products",
                column: "SupplierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_products_product_id",
                table: "inventory",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movement_inventory_inventory_id",
                table: "inventory_movement",
                column: "inventory_id",
                principalTable: "inventory",
                principalColumn: "inventory_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movement_products_ProductId1",
                table: "inventory_movement",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movement_products_product_id",
                table: "inventory_movement",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_category_CategoryId1",
                table: "products",
                column: "CategoryId1",
                principalTable: "category",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_category_category_id",
                table: "products",
                column: "category_id",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_material_MaterialId1",
                table: "products",
                column: "MaterialId1",
                principalTable: "material",
                principalColumn: "material_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_material_material_id",
                table: "products",
                column: "material_id",
                principalTable: "material",
                principalColumn: "material_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_supplier_SupplierId1",
                table: "products",
                column: "SupplierId1",
                principalTable: "supplier",
                principalColumn: "supplier_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_supplier_supplier_id",
                table: "products",
                column: "supplier_id",
                principalTable: "supplier",
                principalColumn: "supplier_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_supplier_SupplierId",
                table: "Purchase",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "supplier_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_products_ProductId",
                table: "PurchaseDetail",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Return_sales_SaleId",
                table: "Return",
                column: "SaleId",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnDetail_sale_details_SaleDetailId",
                table: "ReturnDetail",
                column: "SaleDetailId",
                principalTable: "sale_details",
                principalColumn: "sale_detail_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_details_products_ProductId",
                table: "sale_details",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_details_sales_SaleId",
                table: "sale_details",
                column: "SaleId",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sales_Customer_CustomerId",
                table: "sales",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_products_product_id",
                table: "inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movement_inventory_inventory_id",
                table: "inventory_movement");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movement_products_ProductId1",
                table: "inventory_movement");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movement_products_product_id",
                table: "inventory_movement");

            migrationBuilder.DropForeignKey(
                name: "FK_products_category_CategoryId1",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_category_category_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_material_MaterialId1",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_material_material_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_supplier_SupplierId1",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_supplier_supplier_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_supplier_SupplierId",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_products_ProductId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Return_sales_SaleId",
                table: "Return");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnDetail_sale_details_SaleDetailId",
                table: "ReturnDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_details_products_ProductId",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_details_sales_SaleId",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_Customer_CustomerId",
                table: "sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_supplier",
                table: "supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sales",
                table: "sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryId1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_MaterialId1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SupplierId1",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_material",
                table: "material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sale_details",
                table: "sale_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_movement",
                table: "inventory_movement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory",
                table: "inventory");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "products");

            migrationBuilder.DropColumn(
                name: "MaterialId1",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SupplierId1",
                table: "products");

            migrationBuilder.RenameTable(
                name: "supplier",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "sales",
                newName: "Sales");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "material",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "sale_details",
                newName: "SaleDetails");

            migrationBuilder.RenameTable(
                name: "inventory_movement",
                newName: "InventoryMovements");

            migrationBuilder.RenameTable(
                name: "inventory",
                newName: "Inventories");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Supplier",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Supplier",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Supplier",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Supplier",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Supplier",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Supplier",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "ruc_dni",
                table: "Supplier",
                newName: "RucDni");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Supplier",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Supplier",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "Supplier",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Sales",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "tax_amount",
                table: "Sales",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "subtotal_amount",
                table: "Sales",
                newName: "SubtotalAmount");

            migrationBuilder.RenameColumn(
                name: "sale_number",
                table: "Sales",
                newName: "SaleNumber");

            migrationBuilder.RenameColumn(
                name: "discount_amount",
                table: "Sales",
                newName: "DiscountAmount");

            migrationBuilder.RenameColumn(
                name: "sale_id",
                table: "Sales",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_sales_CustomerId",
                table: "Sales",
                newName: "IX_Sales_CustomerId");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Products",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Products",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "sku",
                table: "Products",
                newName: "Sku");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Products",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "certificate",
                table: "Products",
                newName: "Certificate");

            migrationBuilder.RenameColumn(
                name: "barcode",
                table: "Products",
                newName: "Barcode");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Products",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "Products",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "selling_price",
                table: "Products",
                newName: "SellingPrice");

            migrationBuilder.RenameColumn(
                name: "qr_code",
                table: "Products",
                newName: "QrCode");

            migrationBuilder.RenameColumn(
                name: "material_id",
                table: "Products",
                newName: "MaterialId");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Products",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cost_price",
                table: "Products",
                newName: "CostPrice");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_products_supplier_id",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_products_material_id",
                table: "Products",
                newName: "IX_Products_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Material",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Material",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "purity",
                table: "Material",
                newName: "Purity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Material",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Material",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "reference_price",
                table: "Material",
                newName: "ReferencePrice");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Material",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "material_id",
                table: "Material",
                newName: "MaterialId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Category",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Category",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Category",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Category",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Category",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Category",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "SaleDetails",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "SaleDetails",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "line_discount",
                table: "SaleDetails",
                newName: "LineDiscount");

            migrationBuilder.RenameColumn(
                name: "sale_detail_id",
                table: "SaleDetails",
                newName: "SaleDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_sale_details_SaleId",
                table: "SaleDetails",
                newName: "IX_SaleDetails_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_sale_details_ProductId",
                table: "SaleDetails",
                newName: "IX_SaleDetails_ProductId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "InventoryMovements",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "movement_type",
                table: "InventoryMovements",
                newName: "MovementType");

            migrationBuilder.RenameColumn(
                name: "inventory_id",
                table: "InventoryMovements",
                newName: "InventoryId");

            migrationBuilder.RenameColumn(
                name: "inventory_movement_id",
                table: "InventoryMovements",
                newName: "InventoryMovementId");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movement_ProductId1",
                table: "InventoryMovements",
                newName: "IX_InventoryMovements_ProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movement_product_id",
                table: "InventoryMovements",
                newName: "IX_InventoryMovements_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movement_inventory_id",
                table: "InventoryMovements",
                newName: "IX_InventoryMovements_InventoryId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Inventories",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "inventory_id",
                table: "Inventories",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_product_id",
                table: "Inventories",
                newName: "IX_Inventories_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Products",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "SaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails",
                column: "SaleDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryMovements",
                table: "InventoryMovements",
                column: "InventoryMovementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovements_Inventories_InventoryId",
                table: "InventoryMovements",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovements_Products_ProductId",
                table: "InventoryMovements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovements_Products_ProductId1",
                table: "InventoryMovements",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Material_MaterialId",
                table: "Products",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplier_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Supplier_SupplierId",
                table: "Purchase",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Products_ProductId",
                table: "PurchaseDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Return_Sales_SaleId",
                table: "Return",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnDetail_SaleDetails_SaleDetailId",
                table: "ReturnDetail",
                column: "SaleDetailId",
                principalTable: "SaleDetails",
                principalColumn: "SaleDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customer_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");
        }
    }
}
