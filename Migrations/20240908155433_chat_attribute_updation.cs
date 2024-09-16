using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Migrations
{
    public partial class chat_attribute_updation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "chatMessages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "chatMessages");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "chatMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "chatMessages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "chatMessages");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "chatMessages");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "chatMessages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "chatMessages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
