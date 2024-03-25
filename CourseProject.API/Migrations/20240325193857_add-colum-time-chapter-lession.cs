using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addcolumtimechapterlession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalTimeLession",
                table: "Lession",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalTimeChapter",
                table: "Chapter",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalTimeLession",
                table: "Lession");

            migrationBuilder.DropColumn(
                name: "TotalTimeChapter",
                table: "Chapter");
        }
    }
}
