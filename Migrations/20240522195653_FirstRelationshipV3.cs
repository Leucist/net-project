using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_platform.Migrations
{
    /// <inheritdoc />
    public partial class FirstRelationshipV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_IdStudent",
                table: "Enrollments",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UsersUserId",
                table: "Enrollments",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_IdCourse",
                table: "Enrollments",
                column: "IdCourse",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_IdStudent",
                table: "Enrollments",
                column: "IdStudent",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UsersUserId",
                table: "Enrollments",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_IdCourse",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_IdStudent",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UsersUserId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_IdStudent",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UsersUserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Enrollments");

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
    }
}
