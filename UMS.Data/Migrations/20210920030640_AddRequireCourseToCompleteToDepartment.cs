using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class AddRequireCourseToCompleteToDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequireCourseToComplete",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequireCourseToComplete",
                table: "Departments");
        }
    }
}
