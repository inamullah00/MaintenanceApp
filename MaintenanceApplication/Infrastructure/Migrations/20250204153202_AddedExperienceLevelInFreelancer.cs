using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedExperienceLevelInFreelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bda9dc53-b6ce-4db3-8a17-b10c3642b005");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c67d0642-4b37-49e2-8282-e9a54bdde5d1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "222e458a-d461-449f-8359-f93610a3732a", "b9d1f1c3-9ed9-43a0-8a0c-012254ff0522" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "222e458a-d461-449f-8359-f93610a3732a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9d1f1c3-9ed9-43a0-8a0c-012254ff0522");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceLevel",
                table: "Freelancers",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ExperienceLevel",
                table: "Freelancers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "222e458a-d461-449f-8359-f93610a3732a", null, "Admin", "ADMIN" },
                    { "bda9dc53-b6ce-4db3-8a17-b10c3642b005", null, "Freelancer", "FREELANCER" },
                    { "c67d0642-4b37-49e2-8282-e9a54bdde5d1", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b9d1f1c3-9ed9-43a0-8a0c-012254ff0522", 0, "74a81581-9d7a-4706-8e7a-297d6206e4a3", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEI60na5q3VR7HY/aqKY7riaW3Sdzl8KiVdrojSdBw12nEZNfkJVFbiuCv3bkT0JMfA==", null, false, "f00775d0-1f6f-45c8-a22f-35fe807a2bae", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "222e458a-d461-449f-8359-f93610a3732a", "b9d1f1c3-9ed9-43a0-8a0c-012254ff0522" });
        }
    }
}
