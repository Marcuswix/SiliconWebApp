using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FourthFixingAddingCoursesAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaceBookFollowers",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeSubscribers",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailTitle",
                table: "ProgramDetailsItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfoOne",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfoThree",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfoTwo",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfArticles",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resourses",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FaceBookFollowers",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "YoutubeSubscribers",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DetailTitle",
                table: "ProgramDetailsItems");

            migrationBuilder.DropColumn(
                name: "ExtraInfoOne",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExtraInfoThree",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExtraInfoTwo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NumberOfArticles",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Resourses",
                table: "Courses");
        }
    }
}
