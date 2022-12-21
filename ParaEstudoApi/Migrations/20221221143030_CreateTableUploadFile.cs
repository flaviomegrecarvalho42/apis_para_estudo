using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParaEstudoApi.Migrations
{
    public partial class CreateTableUploadFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginName = table.Column<string>(maxLength: 300, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    GeneratedAt = table.Column<DateTime>(nullable: false),
                    HashCode = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadFile_GeneratedAt",
                table: "UploadFile",
                column: "GeneratedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFile_HashCode",
                table: "UploadFile",
                column: "HashCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadFile");
        }
    }
}
