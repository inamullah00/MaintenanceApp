using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPackageEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OfferDetails",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bids");

            migrationBuilder.AlterColumn<int>(
                name: "OtpCode",
                table: "FreelancerOtps",
                type: "int",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<int>(
                name: "OtpCode",
                table: "ClientOtps",
                type: "int",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FreelancerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidPackages",
                columns: table => new
                {
                    BidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidPackages", x => new { x.BidId, x.PackageId });
                    table.ForeignKey(
                        name: "FK_BidPackages_Bids_BidId",
                        column: x => x.BidId,
                        principalTable: "Bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidPackages_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BidPackages_PackageId",
                table: "BidPackages",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_FreelancerId",
                table: "Packages",
                column: "FreelancerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidPackages");

            migrationBuilder.DropTable(
                name: "Packages");

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

            migrationBuilder.AlterColumn<string>(
                name: "OtpCode",
                table: "FreelancerOtps",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "OtpCode",
                table: "ClientOtps",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "Bids",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OfferDetails",
                table: "Bids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Bids",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34a30417-31fc-4216-b315-a21a99389f60", null, "Freelancer", "FREELANCER" },
                    { "94dd1802-29a3-48b0-8679-4d516474fd96", null, "Client", "CLIENT" },
                    { "c7b5fccc-88f1-46e8-b05d-0131848c3d64", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bc9b2f-6401-40c1-9440-72e293f41a12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5bcc1f7-ff49-43f0-9e1c-8a1dd8cd494f", "AQAAAAIAAYagAAAAELyarLRVP/PGR5HvDkpRBDIfEQAX5/Nwn3kNCOp5uEo5XeyRmSINa7HxTxJQipPg/w==", "c00d198a-9d5d-4ad0-ae48-b94f242e1891" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7b5fccc-88f1-46e8-b05d-0131848c3d64", "21bc9b2f-6401-40c1-9440-72e293f41a12" });
        }
    }
}
