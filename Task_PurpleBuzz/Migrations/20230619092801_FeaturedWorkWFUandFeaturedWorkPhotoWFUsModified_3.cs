using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_PurpleBuzz.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedWorkWFUandFeaturedWorkPhotoWFUsModified_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "FeaturedWorkWFU");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FeaturedWorkWFU",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FeaturedWorkPhotoWFUs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "FeaturedWorkPhotoWFUs");

            migrationBuilder.AlterColumn<int>(
                name: "Description",
                table: "FeaturedWorkWFU",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FeaturedWorkWFU",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
