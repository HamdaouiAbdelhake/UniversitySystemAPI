using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueClassroomNumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_ClassroomNumber",
                table: "Courses",
                column: "ClassroomNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_ClassroomNumber",
                table: "Courses");
        }
    }
}
