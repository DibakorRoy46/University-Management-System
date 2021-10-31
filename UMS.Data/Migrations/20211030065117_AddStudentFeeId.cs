using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Data.Migrations
{
    public partial class AddStudentFeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentSemesterFeeId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudentSemesterFeeId",
                table: "Semesters",
                column: "StudentSemesterFeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudentSemesterFees_StudentSemesterFeeId",
                table: "Semesters",
                column: "StudentSemesterFeeId",
                principalTable: "StudentSemesterFees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudentSemesterFees_StudentSemesterFeeId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_StudentSemesterFeeId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StudentSemesterFeeId",
                table: "Semesters");
        }
    }
}
