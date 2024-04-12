using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingAddingCoursesAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fact",
                table: "WhatYouLearn");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "ProgramDetails");

            migrationBuilder.AddColumn<int>(
                name: "WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProgramDetailsItemsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgramDetailsEntityId = table.Column<int>(type: "int", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDetailsItemsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDetailsItemsEntity_ProgramDetails_ProgramDetailsEntityId",
                        column: x => x.ProgramDetailsEntityId,
                        principalTable: "ProgramDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WhatYouLearnItemsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhatYouLearnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhatYouLearnItemsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhatYouLearnItemsEntity_WhatYouLearn_WhatYouLearnId",
                        column: x => x.WhatYouLearnId,
                        principalTable: "WhatYouLearn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhatYouLearn_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                column: "WhatYouLearnItemsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetailsItemsEntity_ProgramDetailsEntityId",
                table: "ProgramDetailsItemsEntity",
                column: "ProgramDetailsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WhatYouLearnItemsEntity_WhatYouLearnId",
                table: "WhatYouLearnItemsEntity",
                column: "WhatYouLearnId");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItemsEntity_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                column: "WhatYouLearnItemsEntityId",
                principalTable: "WhatYouLearnItemsEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItemsEntity_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropTable(
                name: "ProgramDetailsItemsEntity");

            migrationBuilder.DropTable(
                name: "WhatYouLearnItemsEntity");

            migrationBuilder.DropIndex(
                name: "IX_WhatYouLearn_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropColumn(
                name: "WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.AddColumn<string>(
                name: "Fact",
                table: "WhatYouLearn",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "ProgramDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
