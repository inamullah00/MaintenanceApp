using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmptyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1065269c-471e-4ffa-8dc0-6b5e0dd08083");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fd8edcc-f981-498d-8286-4419c9d81c77");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "77f163b3-3501-454d-a109-3c7ae9bd206d", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77f163b3-3501-454d-a109-3c7ae9bd206d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12318891-d3d2-4de1-9b42-3d5ad8096877", null, "Client", "CLIENT" },
                    { "7dc90c0a-1f05-4b95-82cd-f226f83767f0", null, "Admin", "ADMIN" },
                    { "a61e0724-1ae8-4834-9534-dfa9d36ce638", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e744bb9d-96bf-4e44-bdfb-b22e3a1c7cef", "AQAAAAIAAYagAAAAEEz7dH4VJ8y2pEeuvZhCANUQPf995rGgYmXHDOMKCbgiwUbfR7ujoEmrYvkcSMtCGQ==", "155f44e6-12ca-4e69-96c1-53f3d7f47fd5" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7dc90c0a-1f05-4b95-82cd-f226f83767f0", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12318891-d3d2-4de1-9b42-3d5ad8096877");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a61e0724-1ae8-4834-9534-dfa9d36ce638");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7dc90c0a-1f05-4b95-82cd-f226f83767f0", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7dc90c0a-1f05-4b95-82cd-f226f83767f0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1065269c-471e-4ffa-8dc0-6b5e0dd08083", null, "Freelancer", "FREELANCER" },
                    { "1fd8edcc-f981-498d-8286-4419c9d81c77", null, "Client", "CLIENT" },
                    { "77f163b3-3501-454d-a109-3c7ae9bd206d", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf3fb958-fb10-4378-bc88-b1cbe2ab4396", "AQAAAAIAAYagAAAAEH9HO+lhg95tCxFrOSto0d5PGJm6yWkLEhxommrujlTsBFl/npLid/EIGsyYJbo8Cw==", "c7afa4a4-a75c-4818-baff-a44f2bac79d2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "77f163b3-3501-454d-a109-3c7ae9bd206d", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }
    }
}
