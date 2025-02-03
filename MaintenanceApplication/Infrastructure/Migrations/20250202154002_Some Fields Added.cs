using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SomeFieldsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a1cb165-3ef7-4b26-80a6-3935799a7f75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7523748-3324-44bd-9565-12108e39a124");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "be058a74-0327-48f6-adf3-a9009ff6c441", "1791d2c4-968d-4fd4-903a-ec917eb5560f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be058a74-0327-48f6-adf3-a9009ff6c441");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1791d2c4-968d-4fd4-903a-ec917eb5560f");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "BidDate",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CurrentRating",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Bids",
                newName: "CoverLetter");

            migrationBuilder.RenameColumn(
                name: "CustomPrice",
                table: "Bids",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OfferDetails",
                table: "Bids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e038111-104b-4253-9fb3-0f4a227c8ba7", null, "Admin", "ADMIN" },
                    { "26c1f157-2080-4ff4-8489-4313da281c81", null, "Client", "CLIENT" },
                    { "73e7e7b6-6778-4c89-ab6d-03183bc6e212", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4d5e78a0-5fad-42c8-9c13-7aa9a4507db6", 0, "7c992a74-8d8f-4343-921d-b100e99668d7", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKrG/962LEYNr+1hKxGhWKA9jD276eZV5snCmed8K4KtkX20sAGWQTfCn6RO6TbZGQ==", null, false, "bfd8a7cd-049a-4f9b-89c2-fd33a7618521", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0e038111-104b-4253-9fb3-0f4a227c8ba7", "4d5e78a0-5fad-42c8-9c13-7aa9a4507db6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26c1f157-2080-4ff4-8489-4313da281c81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73e7e7b6-6778-4c89-ab6d-03183bc6e212");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e038111-104b-4253-9fb3-0f4a227c8ba7", "4d5e78a0-5fad-42c8-9c13-7aa9a4507db6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e038111-104b-4253-9fb3-0f4a227c8ba7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d5e78a0-5fad-42c8-9c13-7aa9a4507db6");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OfferDetails",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Bids",
                newName: "CustomPrice");

            migrationBuilder.RenameColumn(
                name: "CoverLetter",
                table: "Bids",
                newName: "Message");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Bids",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BidDate",
                table: "Bids",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentRating",
                table: "Bids",
                type: "float",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a1cb165-3ef7-4b26-80a6-3935799a7f75", null, "Freelancer", "FREELANCER" },
                    { "be058a74-0327-48f6-adf3-a9009ff6c441", null, "Admin", "ADMIN" },
                    { "f7523748-3324-44bd-9565-12108e39a124", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1791d2c4-968d-4fd4-903a-ec917eb5560f", 0, "7940b0e6-e14e-400f-a6e6-f1f71cf8e30e", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEBmopGyRYEadU5C3xjiBdG5/t2PtdCGUaGtSOjtjhvNoMlk5yITQgtdHFv/bE1EP9g==", null, false, "72a9ef4b-39fa-4ebf-ad38-7fd4ad25db1e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "be058a74-0327-48f6-adf3-a9009ff6c441", "1791d2c4-968d-4fd4-903a-ec917eb5560f" });
        }
    }
}
