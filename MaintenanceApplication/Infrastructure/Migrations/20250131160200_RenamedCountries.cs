using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Freelancers_Country_CountryId",
                table: "Freelancers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30964f5f-db56-407e-9965-8f3244ce4b66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c50cda6c-fa8a-4041-9760-c753061a82c1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "14cdd25a-c79f-465d-9246-d0b50c9b2ce8", "98d6d110-3b15-48fe-9029-096340d116b2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14cdd25a-c79f-465d-9246-d0b50c9b2ce8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98d6d110-3b15-48fe-9029-096340d116b2");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "288d8c9c-99db-4cd4-962b-84c2d697ff99", null, "Client", "CLIENT" },
                    { "8e42175e-a08d-4c16-9224-cbdf765e99d0", null, "Admin", "ADMIN" },
                    { "a2555a97-a1af-4a42-9afc-e470146de437", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "579693bb-c961-427b-9810-3dacccbf0bb4", 0, "ce8ecb21-208d-444c-952f-98b226c522b8", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIiuuvfD/co0GVZF/ac7tlZ1lznIvVDJZyDkPY1jMLFsGKRC+sOP+nEqsaaRWvIl+g==", null, false, "86d11c07-a28d-4a07-a596-98c192089ac5", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8e42175e-a08d-4c16-9224-cbdf765e99d0", "579693bb-c961-427b-9810-3dacccbf0bb4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancers_Countries_CountryId",
                table: "Freelancers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Freelancers_Countries_CountryId",
                table: "Freelancers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "288d8c9c-99db-4cd4-962b-84c2d697ff99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2555a97-a1af-4a42-9afc-e470146de437");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e42175e-a08d-4c16-9224-cbdf765e99d0", "579693bb-c961-427b-9810-3dacccbf0bb4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e42175e-a08d-4c16-9224-cbdf765e99d0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "579693bb-c961-427b-9810-3dacccbf0bb4");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14cdd25a-c79f-465d-9246-d0b50c9b2ce8", null, "Admin", "ADMIN" },
                    { "30964f5f-db56-407e-9965-8f3244ce4b66", null, "Freelancer", "FREELANCER" },
                    { "c50cda6c-fa8a-4041-9760-c753061a82c1", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "98d6d110-3b15-48fe-9029-096340d116b2", 0, "f0f608f0-eb14-48dd-bc06-781373a81cf8", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOz6TSfELhIWM3Ahrhl8kuJ6H2CXzPMac3teaY6JD5TDF781M62qBHsM9tAnzAjD9w==", null, false, "1bd495fc-7234-4fb8-8fcd-cb1798647494", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "14cdd25a-c79f-465d-9246-d0b50c9b2ce8", "98d6d110-3b15-48fe-9029-096340d116b2" });

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancers_Country_CountryId",
                table: "Freelancers",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id");
        }
    }
}
