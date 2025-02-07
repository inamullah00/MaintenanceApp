using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsInService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerService_Freelancers_FreelancerId",
                table: "FreelancerService");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerService_Service_ServiceId",
                table: "FreelancerService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerService",
                table: "FreelancerService");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f845e3d-43d2-4373-811b-ceab15b3b290");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c2d980c-b414-4948-9e4b-beec614a8c32");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eee74791-4202-4e3d-a56c-64a37491a364", "f6665896-ae75-45d2-a872-cbf78f64f6a4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eee74791-4202-4e3d-a56c-64a37491a364");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6665896-ae75-45d2-a872-cbf78f64f6a4");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "FreelancerService",
                newName: "FreelancerServices");

            migrationBuilder.RenameIndex(
                name: "IX_FreelancerService_ServiceId",
                table: "FreelancerServices",
                newName: "IX_FreelancerServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_FreelancerService_FreelancerId",
                table: "FreelancerServices",
                newName: "IX_FreelancerServices_FreelancerId");

            migrationBuilder.AddColumn<string>(
                name: "ActionById",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserCreated",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerServices",
                table: "FreelancerServices",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34a30417-31fc-4216-b315-a21a99389f60", null, "Freelancer", "FREELANCER" },
                    { "94dd1802-29a3-48b0-8679-4d516474fd96", null, "Client", "CLIENT" },
                    { "c7b5fccc-88f1-46e8-b05d-0131848c3d64", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "21bc9b2f-6401-40c1-9440-72e293f41a12", 0, "c5bcc1f7-ff49-43f0-9e1c-8a1dd8cd494f", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAELyarLRVP/PGR5HvDkpRBDIfEQAX5/Nwn3kNCOp5uEo5XeyRmSINa7HxTxJQipPg/w==", null, false, "c00d198a-9d5d-4ad0-ae48-b94f242e1891", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7b5fccc-88f1-46e8-b05d-0131848c3d64", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ActionById",
                table: "Services",
                column: "ActionById");

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerServices_Freelancers_FreelancerId",
                table: "FreelancerServices",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerServices_Services_ServiceId",
                table: "FreelancerServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_ActionById",
                table: "Services",
                column: "ActionById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerServices_Freelancers_FreelancerId",
                table: "FreelancerServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerServices_Services_ServiceId",
                table: "FreelancerServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_ActionById",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ActionById",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerServices",
                table: "FreelancerServices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34a30417-31fc-4216-b315-a21a99389f60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94dd1802-29a3-48b0-8679-4d516474fd96");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c7b5fccc-88f1-46e8-b05d-0131848c3d64", "21bc9b2f-6401-40c1-9440-72e293f41a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b5fccc-88f1-46e8-b05d-0131848c3d64");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12");

            migrationBuilder.DropColumn(
                name: "ActionById",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsUserCreated",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "FreelancerServices",
                newName: "FreelancerService");

            migrationBuilder.RenameIndex(
                name: "IX_FreelancerServices_ServiceId",
                table: "FreelancerService",
                newName: "IX_FreelancerService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_FreelancerServices_FreelancerId",
                table: "FreelancerService",
                newName: "IX_FreelancerService_FreelancerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerService",
                table: "FreelancerService",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f845e3d-43d2-4373-811b-ceab15b3b290", null, "Freelancer", "FREELANCER" },
                    { "5c2d980c-b414-4948-9e4b-beec614a8c32", null, "Client", "CLIENT" },
                    { "eee74791-4202-4e3d-a56c-64a37491a364", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f6665896-ae75-45d2-a872-cbf78f64f6a4", 0, "d922ece7-d43e-4f6a-a5bb-ebcb429974ae", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEI8juRnCgOlan/gf2cYfqkWEnp4+WCb4W4TFP+/nVvSTShXkzx+yQ+hyiEWKP238zg==", null, false, "fc02773b-13ca-43b3-8958-ef1e147bd001", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eee74791-4202-4e3d-a56c-64a37491a364", "f6665896-ae75-45d2-a872-cbf78f64f6a4" });

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerService_Freelancers_FreelancerId",
                table: "FreelancerService",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerService_Service_ServiceId",
                table: "FreelancerService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
