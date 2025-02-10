using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNoteFieldInFreelancers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Freelancers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a859d951-92ee-492d-a6e4-3ae316b0ac96", null, "Admin", "ADMIN" },
                    { "cfb4187e-9cb3-46a7-93a8-50967525e736", null, "Client", "CLIENT" },
                    { "fef6a520-cc5b-4d94-af15-240d189cd7aa", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3d728da-0b1c-4e5a-9a5d-d5bf0812a862", "AQAAAAIAAYagAAAAEL938ETPbSDyhju+3hkw2ve/MSDhI89XvLq2aFMy48llya/mq+I8aoM2nTw7miSVyQ==", "fe73a590-5194-44f5-a3d2-ffd6f671ced9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a859d951-92ee-492d-a6e4-3ae316b0ac96", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfb4187e-9cb3-46a7-93a8-50967525e736");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fef6a520-cc5b-4d94-af15-240d189cd7aa");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a859d951-92ee-492d-a6e4-3ae316b0ac96", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a859d951-92ee-492d-a6e4-3ae316b0ac96");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Freelancers");

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
    }
}
