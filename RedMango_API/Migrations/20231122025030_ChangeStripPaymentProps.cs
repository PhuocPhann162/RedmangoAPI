using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedMango_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStripPaymentProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripePaymentStatus",
                table: "OrderHeader",
                newName: "StripePaymentIntentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripePaymentIntentID",
                table: "OrderHeader",
                newName: "StripePaymentStatus");
        }
    }
}
