using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfileAndAddressInClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Clients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Clients");

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
    }
}
