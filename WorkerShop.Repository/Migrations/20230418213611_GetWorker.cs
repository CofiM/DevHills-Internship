using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class GetWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Workers",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Workers",
                newName: "IsDeleted");
        }
    }
}
