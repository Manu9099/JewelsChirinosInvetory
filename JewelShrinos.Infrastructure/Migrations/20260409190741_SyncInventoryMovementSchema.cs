using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelShrinos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncInventoryMovementSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_PurchaseDetail_Purchase_PurchaseId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_products_ProductId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_details_products_ProductId",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_details_sales_SaleId",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_Customer_CustomerId",
                table: "sales");

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
                name: "PK_supplier",
                table: "supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseDetail",
                table: "PurchaseDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_material",
                table: "material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_movement",
                table: "inventory_movement");

            migrationBuilder.DropIndex(
                name: "IX_inventory_movement_ProductId1",
                table: "inventory_movement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

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
                newName: "suppliers");

            migrationBuilder.RenameTable(
                name: "PurchaseDetail",
                newName: "purchase_details");

            migrationBuilder.RenameTable(
                name: "Purchase",
                newName: "purchases");

            migrationBuilder.RenameTable(
                name: "material",
                newName: "materials");

            migrationBuilder.RenameTable(
                name: "inventory_movement",
                newName: "inventory_movements");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "categories");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "sales",
                newName: "observations");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "sales",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SunatTicketNumber",
                table: "sales",
                newName: "sunat_ticket_number");

            migrationBuilder.RenameColumn(
                name: "SunatResponseCode",
                table: "sales",
                newName: "sunat_response_code");

            migrationBuilder.RenameColumn(
                name: "SaleStatus",
                table: "sales",
                newName: "sale_status");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "sales",
                newName: "payment_method");

            migrationBuilder.RenameColumn(
                name: "InvoiceUrl",
                table: "sales",
                newName: "invoice_url");

            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "sales",
                newName: "invoice_number");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "sales",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "sales",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "sales",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_sales_CustomerId",
                table: "sales",
                newName: "IX_sales_customer_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "sale_details",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "sale_details",
                newName: "sale_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "sale_details",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "DetailStatus",
                table: "sale_details",
                newName: "detail_status");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "sale_details",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_sale_details_SaleId",
                table: "sale_details",
                newName: "IX_sale_details_sale_id");

            migrationBuilder.RenameIndex(
                name: "IX_sale_details_ProductId",
                table: "sale_details",
                newName: "IX_sale_details_product_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "inventory",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SoldStock",
                table: "inventory",
                newName: "sold_stock");

            migrationBuilder.RenameColumn(
                name: "ReservedStock",
                table: "inventory",
                newName: "reserved_stock");

            migrationBuilder.RenameColumn(
                name: "LastSellingPrice",
                table: "inventory",
                newName: "last_selling_price");

            migrationBuilder.RenameColumn(
                name: "LastSaleDate",
                table: "inventory",
                newName: "last_sale_date");

            migrationBuilder.RenameColumn(
                name: "LastPurchaseDate",
                table: "inventory",
                newName: "last_purchase_date");

            migrationBuilder.RenameColumn(
                name: "LastCostPrice",
                table: "inventory",
                newName: "last_cost_price");

            migrationBuilder.RenameColumn(
                name: "DamagedStock",
                table: "inventory",
                newName: "damaged_stock");

            migrationBuilder.RenameColumn(
                name: "AvailableStock",
                table: "inventory",
                newName: "available_stock");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "purchase_details",
                newName: "subtotal");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "purchase_details",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "purchase_details",
                newName: "observations");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "purchase_details",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "QuantityReceived",
                table: "purchase_details",
                newName: "quantity_received");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "purchase_details",
                newName: "purchase_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "purchase_details",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "purchase_details",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "PurchaseDetailId",
                table: "purchase_details",
                newName: "purchase_detail_id");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_PurchaseId",
                table: "purchase_details",
                newName: "IX_purchase_details_purchase_id");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_ProductId",
                table: "purchase_details",
                newName: "IX_purchase_details_product_id");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "purchases",
                newName: "observations");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "purchases",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "purchases",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "purchases",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "PurchaseStatus",
                table: "purchases",
                newName: "purchase_status");

            migrationBuilder.RenameColumn(
                name: "PurchaseNumber",
                table: "purchases",
                newName: "purchase_number");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "purchases",
                newName: "purchase_date");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "purchases",
                newName: "delivery_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "purchases",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "purchases",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "purchases",
                newName: "purchase_id");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_SupplierId",
                table: "purchases",
                newName: "IX_purchases_supplier_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "inventory_movements",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "inventory_id",
                table: "inventory_movements",
                newName: "InventoryId");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "inventory_movements",
                newName: "reference_type");

            migrationBuilder.RenameColumn(
                name: "ProductId1",
                table: "inventory_movements",
                newName: "stock_before");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "inventory_movements",
                newName: "movement_date");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movement_product_id",
                table: "inventory_movements",
                newName: "IX_inventory_movements_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movement_inventory_id",
                table: "inventory_movements",
                newName: "IX_inventory_movements_InventoryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "sales",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "sales",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal_amount",
                table: "sales",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "sales",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "observations",
                table: "sales",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sunat_ticket_number",
                table: "sales",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sunat_response_code",
                table: "sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sale_status",
                table: "sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "payment_method",
                table: "sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "invoice_url",
                table: "sales",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "invoice_number",
                table: "sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "sales",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "sales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "sale_details",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal",
                table: "sale_details",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "line_discount",
                table: "sale_details",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "detail_status",
                table: "sale_details",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "sale_details",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "products",
                type: "numeric(10,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sku",
                table: "products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "selling_price",
                table: "products",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "qr_code",
                table: "products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "cost_price",
                table: "products",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "certificate",
                table: "products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "barcode",
                table: "products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "last_selling_price",
                table: "inventory",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "last_cost_price",
                table: "inventory",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ruc_dni",
                table: "suppliers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "suppliers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "suppliers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "suppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "contact_name",
                table: "suppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "suppliers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal",
                table: "purchase_details",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observations",
                table: "purchase_details",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "purchase_details",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "observations",
                table: "purchases",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "purchases",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "purchase_status",
                table: "purchases",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "purchase_number",
                table: "purchases",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "purchase_date",
                table: "purchases",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_date",
                table: "purchases",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "purchases",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "materials",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "reference_price",
                table: "materials",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "purity",
                table: "materials",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "materials",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "movement_type",
                table: "inventory_movements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "inventory_movements",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "inventory_movements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "observations",
                table: "inventory_movements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reference_id",
                table: "inventory_movements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stock_after",
                table: "inventory_movements",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "categories",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_suppliers",
                table: "suppliers",
                column: "supplier_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchase_details",
                table: "purchase_details",
                column: "purchase_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchases",
                table: "purchases",
                column: "purchase_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_materials",
                table: "materials",
                column: "material_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_movements",
                table: "inventory_movements",
                column: "inventory_movement_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_ProductId",
                table: "sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_sale_number",
                table: "sales",
                column: "sale_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sale_details_ProductId1",
                table: "sale_details",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_products_barcode",
                table: "products",
                column: "barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_code",
                table: "products",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_qr_code",
                table: "products",
                column: "qr_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_suppliers_name",
                table: "suppliers",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "idx_suppliers_status",
                table: "suppliers",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_ruc_dni",
                table: "suppliers",
                column: "ruc_dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchases_purchase_number",
                table: "purchases",
                column: "purchase_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_materials_purity",
                table: "materials",
                column: "purity");

            migrationBuilder.CreateIndex(
                name: "idx_materials_status",
                table: "materials",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_materials_type",
                table: "materials",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "unique_material_type_purity",
                table: "materials",
                columns: new[] { "type", "purity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_name",
                table: "categories",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_products",
                table: "inventory",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movements_inventory_InventoryId",
                table: "inventory_movements",
                column: "InventoryId",
                principalTable: "inventory",
                principalColumn: "inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movements_products_product_id",
                table: "inventory_movements",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_materials",
                table: "products",
                column: "material_id",
                principalTable: "materials",
                principalColumn: "material_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_suppliers",
                table: "products",
                column: "supplier_id",
                principalTable: "suppliers",
                principalColumn: "supplier_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_purchase_details_products",
                table: "purchase_details",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_purchase_details_purchases",
                table: "purchase_details",
                column: "purchase_id",
                principalTable: "purchases",
                principalColumn: "purchase_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_purchases_suppliers",
                table: "purchases",
                column: "supplier_id",
                principalTable: "suppliers",
                principalColumn: "supplier_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_details_products_ProductId1",
                table: "sale_details",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sale_details_products",
                table: "sale_details",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_sale_details_sales",
                table: "sale_details",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sales_Customer_customer_id",
                table: "sales",
                column: "customer_id",
                principalTable: "Customer",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_products_ProductId",
                table: "sales",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventory_products",
                table: "inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movements_inventory_InventoryId",
                table: "inventory_movements");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movements_products_product_id",
                table: "inventory_movements");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_materials",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_suppliers",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_purchase_details_products",
                table: "purchase_details");

            migrationBuilder.DropForeignKey(
                name: "fk_purchase_details_purchases",
                table: "purchase_details");

            migrationBuilder.DropForeignKey(
                name: "fk_purchases_suppliers",
                table: "purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_details_products_ProductId1",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "fk_sale_details_products",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "fk_sale_details_sales",
                table: "sale_details");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_Customer_customer_id",
                table: "sales");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_products_ProductId",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sales_ProductId",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sales_sale_number",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sale_details_ProductId1",
                table: "sale_details");

            migrationBuilder.DropIndex(
                name: "IX_products_barcode",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_code",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_qr_code",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_suppliers",
                table: "suppliers");

            migrationBuilder.DropIndex(
                name: "idx_suppliers_name",
                table: "suppliers");

            migrationBuilder.DropIndex(
                name: "idx_suppliers_status",
                table: "suppliers");

            migrationBuilder.DropIndex(
                name: "IX_suppliers_ruc_dni",
                table: "suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchases",
                table: "purchases");

            migrationBuilder.DropIndex(
                name: "IX_purchases_purchase_number",
                table: "purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchase_details",
                table: "purchase_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_materials",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "idx_materials_purity",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "idx_materials_status",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "idx_materials_type",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "unique_material_type_purity",
                table: "materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_movements",
                table: "inventory_movements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_name",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "sale_details");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "inventory_movements");

            migrationBuilder.DropColumn(
                name: "observations",
                table: "inventory_movements");

            migrationBuilder.DropColumn(
                name: "reference_id",
                table: "inventory_movements");

            migrationBuilder.DropColumn(
                name: "stock_after",
                table: "inventory_movements");

            migrationBuilder.RenameTable(
                name: "suppliers",
                newName: "supplier");

            migrationBuilder.RenameTable(
                name: "purchases",
                newName: "Purchase");

            migrationBuilder.RenameTable(
                name: "purchase_details",
                newName: "PurchaseDetail");

            migrationBuilder.RenameTable(
                name: "materials",
                newName: "material");

            migrationBuilder.RenameTable(
                name: "inventory_movements",
                newName: "inventory_movement");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "observations",
                table: "sales",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "sales",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "sunat_ticket_number",
                table: "sales",
                newName: "SunatTicketNumber");

            migrationBuilder.RenameColumn(
                name: "sunat_response_code",
                table: "sales",
                newName: "SunatResponseCode");

            migrationBuilder.RenameColumn(
                name: "sale_status",
                table: "sales",
                newName: "SaleStatus");

            migrationBuilder.RenameColumn(
                name: "payment_method",
                table: "sales",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "invoice_url",
                table: "sales",
                newName: "InvoiceUrl");

            migrationBuilder.RenameColumn(
                name: "invoice_number",
                table: "sales",
                newName: "InvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "sales",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "sales",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "sales",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_sales_customer_id",
                table: "sales",
                newName: "IX_sales_CustomerId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "sale_details",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "sale_id",
                table: "sale_details",
                newName: "SaleId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "sale_details",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "detail_status",
                table: "sale_details",
                newName: "DetailStatus");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "sale_details",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_sale_details_sale_id",
                table: "sale_details",
                newName: "IX_sale_details_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_sale_details_product_id",
                table: "sale_details",
                newName: "IX_sale_details_ProductId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "inventory",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "sold_stock",
                table: "inventory",
                newName: "SoldStock");

            migrationBuilder.RenameColumn(
                name: "reserved_stock",
                table: "inventory",
                newName: "ReservedStock");

            migrationBuilder.RenameColumn(
                name: "last_selling_price",
                table: "inventory",
                newName: "LastSellingPrice");

            migrationBuilder.RenameColumn(
                name: "last_sale_date",
                table: "inventory",
                newName: "LastSaleDate");

            migrationBuilder.RenameColumn(
                name: "last_purchase_date",
                table: "inventory",
                newName: "LastPurchaseDate");

            migrationBuilder.RenameColumn(
                name: "last_cost_price",
                table: "inventory",
                newName: "LastCostPrice");

            migrationBuilder.RenameColumn(
                name: "damaged_stock",
                table: "inventory",
                newName: "DamagedStock");

            migrationBuilder.RenameColumn(
                name: "available_stock",
                table: "inventory",
                newName: "AvailableStock");

            migrationBuilder.RenameColumn(
                name: "observations",
                table: "Purchase",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Purchase",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Purchase",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "Purchase",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "purchase_status",
                table: "Purchase",
                newName: "PurchaseStatus");

            migrationBuilder.RenameColumn(
                name: "purchase_number",
                table: "Purchase",
                newName: "PurchaseNumber");

            migrationBuilder.RenameColumn(
                name: "purchase_date",
                table: "Purchase",
                newName: "PurchaseDate");

            migrationBuilder.RenameColumn(
                name: "delivery_date",
                table: "Purchase",
                newName: "DeliveryDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Purchase",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Purchase",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "purchase_id",
                table: "Purchase",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_purchases_supplier_id",
                table: "Purchase",
                newName: "IX_Purchase_SupplierId");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "PurchaseDetail",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "PurchaseDetail",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "observations",
                table: "PurchaseDetail",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "PurchaseDetail",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "quantity_received",
                table: "PurchaseDetail",
                newName: "QuantityReceived");

            migrationBuilder.RenameColumn(
                name: "purchase_id",
                table: "PurchaseDetail",
                newName: "PurchaseId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "PurchaseDetail",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PurchaseDetail",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "purchase_detail_id",
                table: "PurchaseDetail",
                newName: "PurchaseDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_details_purchase_id",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_details_product_id",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_ProductId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "inventory_movement",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "inventory_movement",
                newName: "inventory_id");

            migrationBuilder.RenameColumn(
                name: "stock_before",
                table: "inventory_movement",
                newName: "ProductId1");

            migrationBuilder.RenameColumn(
                name: "reference_type",
                table: "inventory_movement",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "movement_date",
                table: "inventory_movement",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movements_product_id",
                table: "inventory_movement",
                newName: "IX_inventory_movement_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_movements_InventoryId",
                table: "inventory_movement",
                newName: "IX_inventory_movement_inventory_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal_amount",
                table: "sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "SunatTicketNumber",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SunatResponseCode",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SaleStatus",
                table: "sales",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceUrl",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "sales",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "sale_details",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal",
                table: "sale_details",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "line_discount",
                table: "sale_details",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "DetailStatus",
                table: "sale_details",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "products",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sku",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "selling_price",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "qr_code",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "cost_price",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "certificate",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "barcode",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

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

            migrationBuilder.AlterColumn<decimal>(
                name: "LastSellingPrice",
                table: "inventory",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LastCostPrice",
                table: "inventory",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ruc_dni",
                table: "supplier",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "supplier",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "supplier",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "supplier",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "contact_name",
                table: "supplier",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "supplier",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "Purchase",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Purchase",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseStatus",
                table: "Purchase",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseNumber",
                table: "Purchase",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "Purchase",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "Purchase",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Purchase",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "PurchaseDetail",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "PurchaseDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "PurchaseDetail",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "material",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "reference_price",
                table: "material",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "purity",
                table: "material",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "material",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "movement_type",
                table: "inventory_movement",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "inventory_id",
                table: "inventory_movement",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "category",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_supplier",
                table: "supplier",
                column: "supplier_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase",
                column: "PurchaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseDetail",
                table: "PurchaseDetail",
                column: "PurchaseDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_material",
                table: "material",
                column: "material_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_movement",
                table: "inventory_movement",
                column: "inventory_movement_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "category_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movement_ProductId1",
                table: "inventory_movement",
                column: "ProductId1");

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
                name: "FK_PurchaseDetail_Purchase_PurchaseId",
                table: "PurchaseDetail",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_products_ProductId",
                table: "PurchaseDetail",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "product_id",
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
    }
}
