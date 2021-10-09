using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class AddSemesterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreRegistrationCourses_Courses_CourseId",
                table: "PreRegistrationCourses");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "PreRegistrationCourses",
                newName: "PreCourseId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Semesters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CourseforPreregistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseforPreregistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseforPreregistration_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseforPreregistration_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseforPreregistration_CourseId",
                table: "CourseforPreregistration",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseforPreregistration_SemesterId",
                table: "CourseforPreregistration",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreRegistrationCourses_CourseforPreregistration_PreCourseId",
                table: "PreRegistrationCourses",
                column: "PreCourseId",
                principalTable: "CourseforPreregistration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreRegistrationCourses_CourseforPreregistration_PreCourseId",
                table: "PreRegistrationCourses");

            migrationBuilder.DropTable(
                name: "CourseforPreregistration");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Semesters");

            migrationBuilder.RenameColumn(
                name: "PreCourseId",
                table: "PreRegistrationCourses",
                newName: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreRegistrationCourses_Courses_CourseId",
                table: "PreRegistrationCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
