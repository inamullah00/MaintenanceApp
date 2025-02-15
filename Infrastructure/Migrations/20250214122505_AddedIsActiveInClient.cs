using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveInClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9465e5e0-ba5d-41fb-bb0c-7f9f1167cd68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7fb2995-09d9-4b68-9b94-de8f4fe4c4f3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f6eaff71-c954-4792-8159-ae65d5954ee3", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6eaff71-c954-4792-8159-ae65d5954ee3");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bf9e005-57cd-4503-a6c2-1664352aadcf", null, "Freelancer", "FREELANCER" },
                    { "66a6daaf-38c1-4a72-be1c-52f82f9b0c97", null, "Client", "CLIENT" },
                    { "d8fa64ea-7da6-4546-955c-c68621a0aaff", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0c5418d-cf10-4099-a14e-bb1f82f96126", "AQAAAAIAAYagAAAAEPo62J9xs8HK48eac/qo33ObmXKTfT4yF43jZXynHkXuDL/KndU6o/ak0n6mPdcaSg==", "27a9e2e4-964a-4808-8679-55d3c17d43b7" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d8fa64ea-7da6-4546-955c-c68621a0aaff", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9465e5e0-ba5d-41fb-bb0c-7f9f1167cd68", null, "Client", "CLIENT" },
                    { "b7fb2995-09d9-4b68-9b94-de8f4fe4c4f3", null, "Freelancer", "FREELANCER" },
                    { "f6eaff71-c954-4792-8159-ae65d5954ee3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb997020-a0b1-41fd-bff8-1a40209361f6", "AQAAAAIAAYagAAAAEI1T2a7HbmhTikf8SMmuVWq0AkF6pT63fcL7N2D+dG7mCI7UI/eCfypgqa33J0CTWA==", "87a3eeee-0568-4829-96bb-d612b1d7c333" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f6eaff71-c954-4792-8159-ae65d5954ee3", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }
    }
}
