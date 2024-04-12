using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdFixingAddingCoursesAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItems_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropIndex(
                name: "IX_WhatYouLearn_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.DropColumn(
                name: "WhatYouLearnItemsEntityId",
                table: "WhatYouLearn");

            migrationBuilder.AddColumn<string>(
                name: "WhatyoulearnFact",
                table: "WhatYouLearnItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatyoulearnFact",
                table: "WhatYouLearnItems");

            migrationBuilder.AddColumn<int>(
                name: "WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WhatYouLearn_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                column: "WhatYouLearnItemsEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WhatYouLearn_WhatYouLearnItems_WhatYouLearnItemsEntityId",
                table: "WhatYouLearn",
                column: "WhatYouLearnItemsEntityId",
                principalTable: "WhatYouLearnItems",
                principalColumn: "Id");
        }
    }
}
