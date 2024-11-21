using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace milestone3library.Migrations
{
    /// <inheritdoc />
    public partial class sgsgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRestricted",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "FineAmount",
                table: "BookTransactions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FinePaid",
                table: "BookTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRestricted",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FineAmount",
                table: "BookTransactions");

            migrationBuilder.DropColumn(
                name: "FinePaid",
                table: "BookTransactions");
        }
    }
}
