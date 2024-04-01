using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_platform.Migrations
{
    /// <inheritdoc />
    public partial class FirstRelationshipV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_IdStudent",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UsersUserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "IdCourse",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enrollments",
                newName: "IdUsers");

            migrationBuilder.RenameColumn(
                name: "IdStudent",
                table: "Enrollments",
                newName: "IdCourses");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsers",
                table: "Enrollments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "IdCourses",
                table: "Enrollments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "IdCourses", "IdUsers" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_IdUsers",
                table: "Enrollments",
                column: "IdUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_IdCourses",
                table: "Enrollments",
                column: "IdCourses",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_IdUsers",
                table: "Enrollments",
                column: "IdUsers",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_IdCourses",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_IdUsers",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_IdUsers",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdUsers",
                table: "Enrollments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdCourses",
                table: "Enrollments",
                newName: "IdStudent");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "IdStudent",
                table: "Enrollments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCourse",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "IdCourse", "IdStudent" });

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
    }
}
