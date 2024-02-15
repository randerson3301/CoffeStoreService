using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeStore.Modules.Customers.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddressPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "CustomerDeliveryAddresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses",
                column: "ZipCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDeliveryAddresses_CustomerId",
                table: "CustomerDeliveryAddresses",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDeliveryAddresses_CustomerId",
                table: "CustomerDeliveryAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "CustomerDeliveryAddresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDeliveryAddresses",
                table: "CustomerDeliveryAddresses",
                column: "CustomerId");
        }
    }
}
