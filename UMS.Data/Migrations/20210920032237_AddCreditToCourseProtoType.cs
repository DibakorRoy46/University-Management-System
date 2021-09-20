using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class AddCreditToCourseProtoType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Credit",
                table: "CourseProtoTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "CourseProtoTypes");
        }
    }
}
