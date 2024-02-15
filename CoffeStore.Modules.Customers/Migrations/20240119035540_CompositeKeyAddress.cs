using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeStore.Modules.Customers.Migrations
{
    /// <inheritdoc />
    public partial class CompositeKeyAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDeliveryAddresses_CustomerId",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses",
                columns: new[] { "CustomerId", "ZipCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses",
                column: "ZipCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDeliveryAddresses_CustomerId",
                table: "CustomerDeliveryAddresses",
                column: "CustomerId");
        }
    }
}
