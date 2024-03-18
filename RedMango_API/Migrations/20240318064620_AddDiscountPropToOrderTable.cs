using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedMango_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountPropToOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "OrderHeader",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "OrderHeader");
        }
    }
}
