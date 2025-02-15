using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bf9e005-57cd-4503-a6c2-1664352aadcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66a6daaf-38c1-4a72-be1c-52f82f9b0c97");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d8fa64ea-7da6-4546-955c-c68621a0aaff", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8fa64ea-7da6-4546-955c-c68621a0aaff");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bf9e005-57cd-4503-a6c2-1664352aadcf", null, "Freelancer", "FREELANCER" },
                    { "66a6daaf-38c1-4a72-be1c-52f82f9b0c97", null, "Client", "CLIENT" },
                    { "d8fa64ea-7da6-4546-955c-c68621a0aaff", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "21bc9b2f-6401-40c1-9440-72e293f41a12", 0, "c0c5418d-cf10-4099-a14e-bb1f82f96126", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEPo62J9xs8HK48eac/qo33ObmXKTfT4yF43jZXynHkXuDL/KndU6o/ak0n6mPdcaSg==", null, false, "27a9e2e4-964a-4808-8679-55d3c17d43b7", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d8fa64ea-7da6-4546-955c-c68621a0aaff", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }
    }
}
