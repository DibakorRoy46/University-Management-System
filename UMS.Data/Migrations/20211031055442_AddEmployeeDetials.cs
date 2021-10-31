using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class AddEmployeeDetials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "UserDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Batch",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeDetials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeavingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDetials_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetials_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_SemesterId",
                table: "UserDetails",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetials_DepartmentId",
                table: "EmployeeDetials",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetials_UserId",
                table: "EmployeeDetials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Semesters_SemesterId",
                table: "UserDetails",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Semesters_SemesterId",
                table: "UserDetails");

            migrationBuilder.DropTable(
                name: "EmployeeDetials");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_SemesterId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Batch",
                table: "Semesters");
        }
    }
}
