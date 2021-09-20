using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class CourseCourserequireModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoursePrerequisites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrerequisites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseProtoTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseProtoTypes_CourseProtoTypeId",
                        column: x => x.CourseProtoTypeId,
                        principalTable: "CourseProtoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseTypes_CourseTypeId",
                        column: x => x.CourseTypeId,
                        principalTable: "CourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseToCoursePrerequisites",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursePreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseToCoursePrerequisites", x => new { x.CourseId, x.CoursePreId });
                    table.ForeignKey(
                        name: "FK_CourseToCoursePrerequisites_CoursePrerequisites_CoursePreId",
                        column: x => x.CoursePreId,
                        principalTable: "CoursePrerequisites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseToCoursePrerequisites_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentId",
                table: "Departments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseProtoTypeId",
                table: "Courses",
                column: "CourseProtoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseTypeId",
                table: "Courses",
                column: "CourseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseToCoursePrerequisites_CoursePreId",
                table: "CourseToCoursePrerequisites",
                column: "CoursePreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Courses_DepartmentId",
                table: "Departments",
                column: "DepartmentId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Courses_DepartmentId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "CourseToCoursePrerequisites");

            migrationBuilder.DropTable(
                name: "CoursePrerequisites");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Departments");
        }
    }
}
