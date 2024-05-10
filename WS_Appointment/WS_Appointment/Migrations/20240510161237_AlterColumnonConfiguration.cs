using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WS_Appointment.Migrations
{
    public partial class AlterColumnonConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "configuration",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "configuration");
        }
    }
}
