using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WS_Appointment.Migrations
{
    public partial class AlterColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "appointmentNo",
                table: "appointments",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "appointments",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appointmentNo",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "token",
                table: "appointments");
        }
    }
}
