using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_platform.Migrations
{
    /// <inheritdoc />
    public partial class FirstRelationshipV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentsUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Enrollments");

            migrationBuilder.CreateTable(
                name: "EnrollmentsUsers",
                columns: table => new
                {
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentsIdCourse = table.Column<int>(type: "int", nullable: false),
                    EnrollmentsIdStudent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentsUsers", x => new { x.UsersUserId, x.EnrollmentsIdCourse, x.EnrollmentsIdStudent });
                    table.ForeignKey(
                        name: "FK_EnrollmentsUsers_Enrollments_EnrollmentsIdCourse_EnrollmentsIdStudent",
                        columns: x => new { x.EnrollmentsIdCourse, x.EnrollmentsIdStudent },
                        principalTable: "Enrollments",
                        principalColumns: new[] { "IdCourse", "IdStudent" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentsUsers_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentsUsers_EnrollmentsIdCourse_EnrollmentsIdStudent",
                table: "EnrollmentsUsers",
                columns: new[] { "EnrollmentsIdCourse", "EnrollmentsIdStudent" });
        }
    }
}
