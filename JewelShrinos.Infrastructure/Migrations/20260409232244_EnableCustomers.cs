using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelShrinos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnableCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movements_inventory_InventoryId",
                table: "inventory_movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Return_Customer_CustomerId",
                table: "Return");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_details_products_ProductId1",
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
                name: "IX_sale_details_ProductId1",
                table: "sale_details");

            migrationBuilder.DropIndex(
                name: "IX_inventory_movements_InventoryId",
                table: "inventory_movements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "sale_details");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "inventory_movements");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "customers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "inventory_movements",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "inventory_movement_id",
                table: "inventory_movements",
                newName: "movement_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "customers",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "customers",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "customers",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "customers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "customers",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "customers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RucDni",
                table: "customers",
                newName: "ruc_dni");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "customers",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "customers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "GoogleProfilePicture",
                table: "customers",
                newName: "google_profile_picture");

            migrationBuilder.RenameColumn(
                name: "GoogleId",
                table: "customers",
                newName: "google_id");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "customers",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "customers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "customers",
                newName: "customer_id");

            migrationBuilder.AlterColumn<string>(
                name: "reference_type",
                table: "inventory_movements",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observations",
                table: "inventory_movements",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "movement_type",
                table: "inventory_movements",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "customers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "customers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "customers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "customers",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ruc_dni",
                table: "customers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ux_customers_email",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_customers_google_id",
                table: "customers",
                column: "google_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Return_customers_CustomerId",
                table: "Return",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_customers_customer_id",
                table: "sales",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Return_customers_CustomerId",
                table: "Return");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_customers_customer_id",
                table: "sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ux_customers_email",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ux_customers_google_id",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "inventory_movements",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "movement_id",
                table: "inventory_movements",
                newName: "inventory_movement_id");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Customer",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Customer",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Customer",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Customer",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Customer",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Customer",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "ruc_dni",
                table: "Customer",
                newName: "RucDni");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Customer",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "google_profile_picture",
                table: "Customer",
                newName: "GoogleProfilePicture");

            migrationBuilder.RenameColumn(
                name: "google_id",
                table: "Customer",
                newName: "GoogleId");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Customer",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Customer",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Customer",
                newName: "CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "sales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "sale_details",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reference_type",
                table: "inventory_movements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observations",
                table: "inventory_movements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "movement_type",
                table: "inventory_movements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "inventory_movements",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RucDni",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_ProductId",
                table: "sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_details_ProductId1",
                table: "sale_details",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_InventoryId",
                table: "inventory_movements",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movements_inventory_InventoryId",
                table: "inventory_movements",
                column: "InventoryId",
                principalTable: "inventory",
                principalColumn: "inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Return_Customer_CustomerId",
                table: "Return",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_details_products_ProductId1",
                table: "sale_details",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "product_id");

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
    }
}
