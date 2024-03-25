using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnlession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalTimeChapter",
                table: "Chapter");

            migrationBuilder.AddColumn<string>(
                name: "LessionName",
                table: "Lession",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessionName",
                table: "Lession");

            migrationBuilder.AddColumn<long>(
                name: "TotalTimeChapter",
                table: "Chapter",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
