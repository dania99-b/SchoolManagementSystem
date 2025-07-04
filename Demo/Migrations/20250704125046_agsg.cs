using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class agsg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddForeignKey(
     name: "FK_Grades_Users_student_id",
     table: "Grades",
     column: "student_id",
     principalTable: "Users",
     principalColumn: "id",
     onDelete: ReferentialAction.Restrict); // ✅ يمنع الـ cycle

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_student_id",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_student_id",
                table: "Grades",
                column: "student_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
