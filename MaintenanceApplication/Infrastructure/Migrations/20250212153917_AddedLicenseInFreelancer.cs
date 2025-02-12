using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLicenseInFreelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CompanyLicense",
                table: "Freelancers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0acda685-f109-4f5c-802a-f38b5805d4e1", null, "Admin", "ADMIN" },
                    { "bfa48b89-6c88-4478-bc51-b6900303a518", null, "Client", "CLIENT" },
                    { "f68f2423-d2d2-4830-b5a1-ba43a1bed6a5", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1222ae80-574a-4e24-acfd-248f7f2c00fc", "AQAAAAIAAYagAAAAEDud91i4IqBgn7Mt+q/oOXrEmGhWjqk4t/T6/TCHyQsVRkjPjYNhf8Nkz5U7RnOTww==", "9a110053-d983-4af3-90b8-7c514f14498a" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0acda685-f109-4f5c-802a-f38b5805d4e1", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfa48b89-6c88-4478-bc51-b6900303a518");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f68f2423-d2d2-4830-b5a1-ba43a1bed6a5");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0acda685-f109-4f5c-802a-f38b5805d4e1", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0acda685-f109-4f5c-802a-f38b5805d4e1");

            migrationBuilder.DropColumn(
                name: "CompanyLicense",
                table: "Freelancers");

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
    }
}
