using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondFixingAddingCoursesAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDetailsItemsEntity_ProgramDetails_ProgramDetailsEntityId",
                table: "ProgramDetailsItemsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItemsEntity_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearnItemsEntity_WhatYouLearn_WhatYouLearnId",
                table: "WhatYouLearnItemsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhatYouLearnItemsEntity",
                table: "WhatYouLearnItemsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramDetailsItemsEntity",
                table: "ProgramDetailsItemsEntity");

            migrationBuilder.RenameTable(
                name: "WhatYouLearnItemsEntity",
                newName: "WhatYouLearnItems");

            migrationBuilder.RenameTable(
                name: "ProgramDetailsItemsEntity",
                newName: "ProgramDetailsItems");

            migrationBuilder.RenameIndex(
                name: "IX_WhatYouLearnItemsEntity_WhatYouLearnId",
                table: "WhatYouLearnItems",
                newName: "IX_WhatYouLearnItems_WhatYouLearnId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDetailsItemsEntity_ProgramDetailsEntityId",
                table: "ProgramDetailsItems",
                newName: "IX_ProgramDetailsItems_ProgramDetailsEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Teachers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhatYouLearnItems",
                table: "WhatYouLearnItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramDetailsItems",
                table: "ProgramDetailsItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDetailsItems_ProgramDetails_ProgramDetailsEntityId",
                table: "ProgramDetailsItems",
                column: "ProgramDetailsEntityId",
                principalTable: "ProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItems_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                column: "WhatYouLearnItemsEntityId",
                principalTable: "WhatYouLearnItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearnItems_WhatYouLearn_WhatYouLearnId",
                table: "WhatYouLearnItems",
                column: "WhatYouLearnId",
                principalTable: "WhatYouLearn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDetailsItems_ProgramDetails_ProgramDetailsEntityId",
                table: "ProgramDetailsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItems_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearnItems_WhatYouLearn_WhatYouLearnId",
                table: "WhatYouLearnItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhatYouLearnItems",
                table: "WhatYouLearnItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramDetailsItems",
                table: "ProgramDetailsItems");

            migrationBuilder.RenameTable(
                name: "WhatYouLearnItems",
                newName: "WhatYouLearnItemsEntity");

            migrationBuilder.RenameTable(
                name: "ProgramDetailsItems",
                newName: "ProgramDetailsItemsEntity");

            migrationBuilder.RenameIndex(
                name: "IX_WhatYouLearnItems_WhatYouLearnId",
                table: "WhatYouLearnItemsEntity",
                newName: "IX_WhatYouLearnItemsEntity_WhatYouLearnId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDetailsItems_ProgramDetailsEntityId",
                table: "ProgramDetailsItemsEntity",
                newName: "IX_ProgramDetailsItemsEntity_ProgramDetailsEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhatYouLearnItemsEntity",
                table: "WhatYouLearnItemsEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramDetailsItemsEntity",
                table: "ProgramDetailsItemsEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDetailsItemsEntity_ProgramDetails_ProgramDetailsEntityId",
                table: "ProgramDetailsItemsEntity",
                column: "ProgramDetailsEntityId",
                principalTable: "ProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItemsEntity_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                column: "WhatYouLearnItemsEntityId",
                principalTable: "WhatYouLearnItemsEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearnItemsEntity_WhatYouLearn_WhatYouLearnId",
                table: "WhatYouLearnItemsEntity",
                column: "WhatYouLearnId",
                principalTable: "WhatYouLearn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
