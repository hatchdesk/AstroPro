using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Image_Add_InArticleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "PageContents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Articles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "PageContents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }
    }
}
