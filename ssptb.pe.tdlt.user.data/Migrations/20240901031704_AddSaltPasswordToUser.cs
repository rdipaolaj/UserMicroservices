using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ssptb.pe.tdlt.user.data.Migrations
{
    /// <inheritdoc />
    public partial class AddSaltPasswordToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaltPassword",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaltPassword",
                table: "Users");
        }
    }
}
