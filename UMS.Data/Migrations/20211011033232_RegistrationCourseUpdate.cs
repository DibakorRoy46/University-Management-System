using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class RegistrationCourseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AssignmentMark",
                table: "StudentRegisteationCourses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AttendanceMark",
                table: "StudentRegisteationCourses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalTermMark",
                table: "StudentRegisteationCourses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "StudentRegisteationCourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MidTermMark",
                table: "StudentRegisteationCourses",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentMark",
                table: "StudentRegisteationCourses");

            migrationBuilder.DropColumn(
                name: "AttendanceMark",
                table: "StudentRegisteationCourses");

            migrationBuilder.DropColumn(
                name: "FinalTermMark",
                table: "StudentRegisteationCourses");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentRegisteationCourses");

            migrationBuilder.DropColumn(
                name: "MidTermMark",
                table: "StudentRegisteationCourses");
        }
    }
}
