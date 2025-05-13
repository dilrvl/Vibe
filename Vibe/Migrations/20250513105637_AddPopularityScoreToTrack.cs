using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vibe.Migrations
{
    /// <inheritdoc />
    public partial class AddPopularityScoreToTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PopularityScore",
                table: "Tracks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PopularityScore",
                table: "Tracks");
        }
    }
}
