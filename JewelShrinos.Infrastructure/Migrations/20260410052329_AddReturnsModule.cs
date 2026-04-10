using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelShrinos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReturnsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Return_customers_CustomerId",
                table: "Return");

            migrationBuilder.DropForeignKey(
                name: "FK_Return_sales_SaleId",
                table: "Return");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnDetail_Return_ReturnId",
                table: "ReturnDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnDetail_sale_details_SaleDetailId",
                table: "ReturnDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReturnDetail",
                table: "ReturnDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Return",
                table: "Return");

            migrationBuilder.RenameTable(
                name: "ReturnDetail",
                newName: "return_details");

            migrationBuilder.RenameTable(
                name: "Return",
                newName: "returns");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "return_details",
                newName: "subtotal");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "return_details",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "SaleDetailId",
                table: "return_details",
                newName: "sale_detail_id");

            migrationBuilder.RenameColumn(
                name: "ReturnId",
                table: "return_details",
                newName: "return_id");

            migrationBuilder.RenameColumn(
                name: "QuantityReturned",
                table: "return_details",
                newName: "quantity_returned");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "return_details",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ReturnDetailId",
                table: "return_details",
                newName: "return_detail_id");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnDetail_SaleDetailId",
                table: "return_details",
                newName: "IX_return_details_sale_detail_id");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnDetail_ReturnId",
                table: "return_details",
                newName: "IX_return_details_return_id");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "returns",
                newName: "reason");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "returns",
                newName: "observations");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "returns",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "returns",
                newName: "sale_id");

            migrationBuilder.RenameColumn(
                name: "ReturnStatus",
                table: "returns",
                newName: "return_status");

            migrationBuilder.RenameColumn(
                name: "ReturnNumber",
                table: "returns",
                newName: "return_number");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "returns",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "RefundAmount",
                table: "returns",
                newName: "refund_amount");

            migrationBuilder.RenameColumn(
                name: "ProcessingDate",
                table: "returns",
                newName: "processing_date");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "returns",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "returns",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "returns",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ReturnId",
                table: "returns",
                newName: "return_id");

            migrationBuilder.RenameIndex(
                name: "IX_Return_SaleId",
                table: "returns",
                newName: "IX_returns_sale_id");

            migrationBuilder.RenameIndex(
                name: "IX_Return_CustomerId",
                table: "returns",
                newName: "IX_returns_customer_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal",
                table: "return_details",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "return_details",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "returns",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "observations",
                table: "returns",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "return_status",
                table: "returns",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "return_number",
                table: "returns",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "refund_amount",
                table: "returns",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "returns",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_return_details",
                table: "return_details",
                column: "return_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_returns",
                table: "returns",
                column: "return_id");

            migrationBuilder.CreateIndex(
                name: "idx_returns_return_status",
                table: "returns",
                column: "return_status");

            migrationBuilder.CreateIndex(
                name: "ux_returns_return_number",
                table: "returns",
                column: "return_number",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_return_details_returns",
                table: "return_details",
                column: "return_id",
                principalTable: "returns",
                principalColumn: "return_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_return_details_sale_details",
                table: "return_details",
                column: "sale_detail_id",
                principalTable: "sale_details",
                principalColumn: "sale_detail_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_returns_customers",
                table: "returns",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_returns_sales",
                table: "returns",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_return_details_returns",
                table: "return_details");

            migrationBuilder.DropForeignKey(
                name: "fk_return_details_sale_details",
                table: "return_details");

            migrationBuilder.DropForeignKey(
                name: "fk_returns_customers",
                table: "returns");

            migrationBuilder.DropForeignKey(
                name: "fk_returns_sales",
                table: "returns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_returns",
                table: "returns");

            migrationBuilder.DropIndex(
                name: "idx_returns_return_status",
                table: "returns");

            migrationBuilder.DropIndex(
                name: "ux_returns_return_number",
                table: "returns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_return_details",
                table: "return_details");

            migrationBuilder.RenameTable(
                name: "returns",
                newName: "Return");

            migrationBuilder.RenameTable(
                name: "return_details",
                newName: "ReturnDetail");

            migrationBuilder.RenameColumn(
                name: "reason",
                table: "Return",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "observations",
                table: "Return",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Return",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "sale_id",
                table: "Return",
                newName: "SaleId");

            migrationBuilder.RenameColumn(
                name: "return_status",
                table: "Return",
                newName: "ReturnStatus");

            migrationBuilder.RenameColumn(
                name: "return_number",
                table: "Return",
                newName: "ReturnNumber");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Return",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "refund_amount",
                table: "Return",
                newName: "RefundAmount");

            migrationBuilder.RenameColumn(
                name: "processing_date",
                table: "Return",
                newName: "ProcessingDate");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Return",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Return",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Return",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "return_id",
                table: "Return",
                newName: "ReturnId");

            migrationBuilder.RenameIndex(
                name: "IX_returns_sale_id",
                table: "Return",
                newName: "IX_Return_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_returns_customer_id",
                table: "Return",
                newName: "IX_Return_CustomerId");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "ReturnDetail",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "ReturnDetail",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "sale_detail_id",
                table: "ReturnDetail",
                newName: "SaleDetailId");

            migrationBuilder.RenameColumn(
                name: "return_id",
                table: "ReturnDetail",
                newName: "ReturnId");

            migrationBuilder.RenameColumn(
                name: "quantity_returned",
                table: "ReturnDetail",
                newName: "QuantityReturned");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ReturnDetail",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "return_detail_id",
                table: "ReturnDetail",
                newName: "ReturnDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_return_details_sale_detail_id",
                table: "ReturnDetail",
                newName: "IX_ReturnDetail_SaleDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_return_details_return_id",
                table: "ReturnDetail",
                newName: "IX_ReturnDetail_ReturnId");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Return",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "Return",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReturnStatus",
                table: "Return",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ReturnNumber",
                table: "Return",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "Return",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Return",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "ReturnDetail",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "ReturnDetail",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Return",
                table: "Return",
                column: "ReturnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReturnDetail",
                table: "ReturnDetail",
                column: "ReturnDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Return_customers_CustomerId",
                table: "Return",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Return_sales_SaleId",
                table: "Return",
                column: "SaleId",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnDetail_Return_ReturnId",
                table: "ReturnDetail",
                column: "ReturnId",
                principalTable: "Return",
                principalColumn: "ReturnId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnDetail_sale_details_SaleDetailId",
                table: "ReturnDetail",
                column: "SaleDetailId",
                principalTable: "sale_details",
                principalColumn: "sale_detail_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
