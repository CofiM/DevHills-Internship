using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AuditLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleVIN",
                table: "WorkOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workerId",
                table: "WorkOrders",
                type: "nvarchar(13)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_VehicleVIN",
                table: "WorkOrders",
                column: "VehicleVIN");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_workerId",
                table: "WorkOrders",
                column: "workerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Vehicles_VehicleVIN",
                table: "WorkOrders",
                column: "VehicleVIN",
                principalTable: "Vehicles",
                principalColumn: "VIN");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Workers_workerId",
                table: "WorkOrders",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Vehicles_VehicleVIN",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Workers_workerId",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_VehicleVIN",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_workerId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "VehicleVIN",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "workerId",
                table: "WorkOrders");
        }
    }
}
