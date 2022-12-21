using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParaEstudoApi.Migrations
{
    public partial class CreateTableDownloadFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DownloadFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    GeneratedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadFile_CreatedAt",
                table: "DownloadFile",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadFile_GeneratedAt",
                table: "DownloadFile",
                column: "GeneratedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadFile");
        }
    }
}
