using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class workOrderDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "WorkOrders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WorkOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedOn",
                table: "WorkOrders",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "WorkOrders");
        }
    }
}
