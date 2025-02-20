﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FieldIdAddedinBidPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BidPackages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BidPackages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BidPackages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BidPackages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BidPackages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BidPackages");
        }
    }
}
