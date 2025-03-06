using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameChart.Migrations
{
    /// <inheritdoc />
    public partial class AddReleaseYearToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DlcReleases_Status_StatusId",
                table: "DlcReleases");

            migrationBuilder.AddColumn<int>(
                name: "ReleasedOnYear",
                table: "Games",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleasedOnYear",
                table: "Dlcs",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "DlcReleases",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DlcReleases_Status_StatusId",
                table: "DlcReleases",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DlcReleases_Status_StatusId",
                table: "DlcReleases");

            migrationBuilder.DropColumn(
                name: "ReleasedOnYear",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReleasedOnYear",
                table: "Dlcs");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "DlcReleases",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_DlcReleases_Status_StatusId",
                table: "DlcReleases",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }
    }
}
