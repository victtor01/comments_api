using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tasks_api.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "User",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "dicimail(18,2)"
            );

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Name", table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "User",
                type: "dicimail(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER"
            );
        }
    }
}
