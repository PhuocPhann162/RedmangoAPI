using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RedMango_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Description", "MenuItemId", "Stars", "Title" },
                values: new object[,]
                {
                    { 1, "I have never tried it before", 2, 5, "Really Delicious!!" },
                    { 2, "I love this product! It exceeded my expectations.", 2, 4, "" },
                    { 3, "I love this product! It exceeded my expectations.", 2, 4, "Great Food!!" },
                    { 4, "I will try again soon", 1, 5, "So Tasteful!!" },
                    { 5, "Just a simple food", 3, 3, "Worst than Vietnamese Food" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
