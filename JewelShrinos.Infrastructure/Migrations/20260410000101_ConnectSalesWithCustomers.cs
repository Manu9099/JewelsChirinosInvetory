using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelShrinos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConnectSalesWithCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_customers_google_id",
                table: "customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ux_customers_google_id",
                table: "customers",
                column: "google_id",
                unique: true);
        }
    }
}
