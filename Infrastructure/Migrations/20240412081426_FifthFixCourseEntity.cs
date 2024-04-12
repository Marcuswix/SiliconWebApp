using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FifthFixCourseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDetails_Courses_CourseEntityId",
                table: "ProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDetailsItems_ProgramDetails_ProgramDetailsEntityId",
                table: "ProgramDetailsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearn_Courses_CourseEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropIndex(
                name: "IX_WhatYouLearn_CourseEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDetailsItems_ProgramDetailsEntityId",
                table: "ProgramDetailsItems");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDetails_CourseEntityId",
                table: "ProgramDetails");

            migrationBuilder.DropColumn(
                name: "CourseEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropColumn(
                name: "ProgramDetailsEntityId",
                table: "ProgramDetailsItems");

            migrationBuilder.DropColumn(
                name: "CourseEntityId",
                table: "ProgramDetails");

            migrationBuilder.AlterColumn<string>(
                name: "WhatyoulearnFact",
                table: "WhatYouLearnItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DetailTitle",
                table: "ProgramDetailsItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramDetailsEntityId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhatYouLearnEntityId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetailsItems_ProgramDetailsId",
                table: "ProgramDetailsItems",
                column: "ProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProgramDetailsEntityId",
                table: "Courses",
                column: "ProgramDetailsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_WhatYouLearnEntityId",
                table: "Courses",
                column: "WhatYouLearnEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ProgramDetails_ProgramDetailsEntityId",
                table: "Courses",
                column: "ProgramDetailsEntityId",
                principalTable: "ProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_WhatYouLearn_WhatYouLearnEntityId",
                table: "Courses",
                column: "WhatYouLearnEntityId",
                principalTable: "WhatYouLearn",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDetailsItems_ProgramDetails_ProgramDetailsId",
                table: "ProgramDetailsItems",
                column: "ProgramDetailsId",
                principalTable: "ProgramDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ProgramDetails_ProgramDetailsEntityId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_WhatYouLearn_WhatYouLearnEntityId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDetailsItems_ProgramDetails_ProgramDetailsId",
                table: "ProgramDetailsItems");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDetailsItems_ProgramDetailsId",
                table: "ProgramDetailsItems");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProgramDetailsEntityId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_WhatYouLearnEntityId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProgramDetailsEntityId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WhatYouLearnEntityId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "WhatyoulearnFact",
                table: "WhatYouLearnItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CourseEntityId",
                table: "WhatYouLearn",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DetailTitle",
                table: "ProgramDetailsItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProgramDetailsEntityId",
                table: "ProgramDetailsItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseEntityId",
                table: "ProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WhatYouLearn_CourseEntityId",
                table: "WhatYouLearn",
                column: "CourseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetailsItems_ProgramDetailsEntityId",
                table: "ProgramDetailsItems",
                column: "ProgramDetailsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetails_CourseEntityId",
                table: "ProgramDetails",
                column: "CourseEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDetails_Courses_CourseEntityId",
                table: "ProgramDetails",
                column: "CourseEntityId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDetailsItems_ProgramDetails_ProgramDetailsEntityId",
                table: "ProgramDetailsItems",
                column: "ProgramDetailsEntityId",
                principalTable: "ProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearn_Courses_CourseEntityId",
                table: "WhatYouLearn",
                column: "CourseEntityId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
