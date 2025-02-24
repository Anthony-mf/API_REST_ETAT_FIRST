using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_REST_ETAT_FIRST.Migrations
{
    /// <inheritdoc />
    public partial class NewAnnotations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "not_note",
                schema: "td4",
                table: "t_j_notation_not",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "not_note",
                schema: "td4",
                table: "t_j_notation_not",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
