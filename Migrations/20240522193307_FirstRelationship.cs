using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_platform.Migrations
{
    /// <inheritdoc />
    public partial class FirstRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    IdCourse = table.Column<int>(type: "int", nullable: false),
                    IdStudent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.IdCourse, x.IdStudent });
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentsUsers");

            migrationBuilder.DropTable(
                name: "Enrollments");
        }
    }
}
