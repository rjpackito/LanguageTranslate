using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    public partial class addgeneratedDLLs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratedDLLs",
                columns: table => new
                {
                    GeneratedDLLId = table.Column<Guid>(nullable: false),
                    GrammaticId = table.Column<Guid>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedDLLs", x => x.GeneratedDLLId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedDLLs");
        }
    }
}
