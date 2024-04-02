using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedMango_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttributesReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_MenuItems_MenuItemId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MenuItemId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Comment", "CreatedAt", "UserId" },
                values: new object[] { "Really Delicious!! I have never tried it before", new DateTime(2024, 4, 2, 9, 37, 11, 111, DateTimeKind.Local).AddTicks(329), "0d65520d-107f-440e-aa41-ed1f492c86ff" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Comment", "CreatedAt", "UserId" },
                values: new object[] { "Yummy!! I love this food. It exceeded my expectations", new DateTime(2024, 4, 2, 9, 37, 11, 111, DateTimeKind.Local).AddTicks(351), "12b39b7b-ae91-437a-b939-56fdb95685f4" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Comment", "CreatedAt", "UserId" },
                values: new object[] { "Great Food!!I love this food. It exceeded my expectations", new DateTime(2024, 4, 2, 9, 37, 11, 111, DateTimeKind.Local).AddTicks(353), "8d9dc5f6-ad81-4558-b5a8-84b3cf4bdca7" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Comment", "CreatedAt", "UserId" },
                values: new object[] { "So Tasteful!! I will try again soon", new DateTime(2024, 4, 2, 9, 37, 11, 111, DateTimeKind.Local).AddTicks(354), "12b39b7b-ae91-437a-b939-56fdb95685f4" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Comment", "CreatedAt", "UserId" },
                values: new object[] { "Worst than Vietnamese Food. Just a simple food", new DateTime(2024, 4, 2, 9, 37, 11, 111, DateTimeKind.Local).AddTicks(355), "20b88091-8f9e-4778-a794-4efc3e16b112" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "I have never tried it before", "Really Delicious!!" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "I love this product! It exceeded my expectations.", "" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Title" },
                values: new object[] { "I love this product! It exceeded my expectations.", "Great Food!!" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Title" },
                values: new object[] { "I will try again soon", "So Tasteful!!" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Just a simple food", "Worst than Vietnamese Food" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MenuItemId",
                table: "Reviews",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_MenuItems_MenuItemId",
                table: "Reviews",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
