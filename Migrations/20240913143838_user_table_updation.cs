using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Migrations
{
    public partial class user_table_updation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Users");
        }
    }
}
