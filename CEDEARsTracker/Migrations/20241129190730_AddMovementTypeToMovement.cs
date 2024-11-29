using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEDEARsTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddMovementTypeToMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovementType",
                table: "Movements",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "Movements");
        }
    }
}
