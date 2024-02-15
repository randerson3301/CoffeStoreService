using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeStore.Modules.Customers.Migrations
{
    /// <inheritdoc />
    public partial class CompositeKeyAddress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses",
                columns: new[] { "CustomerId", "ZipCode", "Number" });
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
                columns: new[] { "CustomerId", "ZipCode" });
        }
    }
}
