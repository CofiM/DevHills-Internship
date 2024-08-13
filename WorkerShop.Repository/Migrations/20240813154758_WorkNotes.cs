using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class WorkNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Workers_workerId",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "workerId",
                table: "WorkOrders",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_workerId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WorkNotes",
                table: "WorkOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Workers_WorkerId",
                table: "WorkOrders",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Workers_WorkerId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkNotes",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkOrders",
                newName: "workerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkerId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_workerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Workers_workerId",
                table: "WorkOrders",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
