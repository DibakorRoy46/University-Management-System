using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class AddSectionTOCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "AssignRegistrationCourses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AssignRegistrationCourses_SectionId",
                table: "AssignRegistrationCourses",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignRegistrationCourses_Sections_SectionId",
                table: "AssignRegistrationCourses",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignRegistrationCourses_Sections_SectionId",
                table: "AssignRegistrationCourses");

            migrationBuilder.DropIndex(
                name: "IX_AssignRegistrationCourses_SectionId",
                table: "AssignRegistrationCourses");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "AssignRegistrationCourses");
        }
    }
}
