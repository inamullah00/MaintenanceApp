using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryIdInClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "039ec9c7-beaf-46a9-9922-b1d29e9cea53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3eb1705e-ed83-4326-a5c2-6a1192eebcac");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7ebb0d53-fb15-4645-af67-9e3d411d3a47", "b20441d0-6d87-49c6-bdb2-fcca352d567a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ebb0d53-fb15-4645-af67-9e3d411d3a47");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b20441d0-6d87-49c6-bdb2-fcca352d567a");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42c60ec7-0f47-4272-b4c3-3b099b096284", null, "Client", "CLIENT" },
                    { "71cf198f-7aa8-4d2c-8091-99c627b42499", null, "Freelancer", "FREELANCER" },
                    { "dc3f9a53-dfff-44a4-a5a1-9c071ddab466", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "914799c3-d7a7-48c3-ae11-fa6970b36a21", 0, "37cfd9ba-99dd-4eee-831e-6ca208a8c444", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFAUfPZXh4unVY+ZLy3353oGzD2dypKTepY6uetmQg4hQ/Z+vVXMxHxYg32Idd0z4Q==", null, false, "f928873e-b889-4854-ad4b-b32fb7b84d81", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dc3f9a53-dfff-44a4-a5a1-9c071ddab466", "914799c3-d7a7-48c3-ae11-fa6970b36a21" });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CountryId",
                table: "Clients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Countries_CountryId",
                table: "Clients",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Countries_CountryId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CountryId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42c60ec7-0f47-4272-b4c3-3b099b096284");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71cf198f-7aa8-4d2c-8091-99c627b42499");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc3f9a53-dfff-44a4-a5a1-9c071ddab466", "914799c3-d7a7-48c3-ae11-fa6970b36a21" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3f9a53-dfff-44a4-a5a1-9c071ddab466");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "914799c3-d7a7-48c3-ae11-fa6970b36a21");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "039ec9c7-beaf-46a9-9922-b1d29e9cea53", null, "Freelancer", "FREELANCER" },
                    { "3eb1705e-ed83-4326-a5c2-6a1192eebcac", null, "Client", "CLIENT" },
                    { "7ebb0d53-fb15-4645-af67-9e3d411d3a47", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b20441d0-6d87-49c6-bdb2-fcca352d567a", 0, "42c6ad9d-762f-42b8-931e-46b75124eb99", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEL+seK0XqAXbrmIReJvYY37VITs1URjCUJk5agTPMivgq6TY9OJbdS2GunjpSMbxtw==", null, false, "18c4b450-5aa7-4e0b-978d-844c79ca9bfe", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7ebb0d53-fb15-4645-af67-9e3d411d3a47", "b20441d0-6d87-49c6-bdb2-fcca352d567a" });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }
    }
}
