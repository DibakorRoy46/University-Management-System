using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class DepartmentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Courses_DepartmentId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentId",
                table: "Departments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Courses_DepartmentId",
                table: "Departments",
                column: "DepartmentId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
