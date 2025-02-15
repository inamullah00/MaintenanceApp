using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ENTITESAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ClientOtps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OtpCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientOtps_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientOtps_Clients_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FreelancerOtps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OtpCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FreelancerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FreelancerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerOtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerOtps_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerOtps_Freelancers_FreelancerId1",
                        column: x => x.FreelancerId1,
                        principalTable: "Freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c292b81-11ec-4161-bf09-232b7bf06cce", null, "Client", "CLIENT" },
                    { "c47a55d9-6b16-4019-a237-a881ee5f1c45", null, "Admin", "ADMIN" },
                    { "e365faa4-40b4-4334-ac92-a403f3c87414", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c", 0, "57aa7056-cb10-41b3-bba8-e340ab3ac124", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEC3Xp9XT718/bSQWd8F+XPVW84e42e2yrpbuECME7bdCiFGl8xyqQvSxTpQwHzM39Q==", null, false, "4069cae5-c28e-4bc9-a7fc-9fa604ea10c8", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c47a55d9-6b16-4019-a237-a881ee5f1c45", "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientOtps_ClientId",
                table: "ClientOtps",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOtps_ClientId1",
                table: "ClientOtps",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOtps_Email",
                table: "ClientOtps",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerOtps_Email",
                table: "FreelancerOtps",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerOtps_FreelancerId",
                table: "FreelancerOtps",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerOtps_FreelancerId1",
                table: "FreelancerOtps",
                column: "FreelancerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientOtps");

            migrationBuilder.DropTable(
                name: "FreelancerOtps");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c292b81-11ec-4161-bf09-232b7bf06cce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e365faa4-40b4-4334-ac92-a403f3c87414");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c47a55d9-6b16-4019-a237-a881ee5f1c45", "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c47a55d9-6b16-4019-a237-a881ee5f1c45");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c");

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
        }
    }
}
