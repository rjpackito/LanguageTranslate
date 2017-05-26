using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    public partial class addhistoryTranslate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryTranslates",
                columns: table => new
                {
                    HistoryTranslateId = table.Column<Guid>(nullable: false),
                    FromLanguage = table.Column<string>(nullable: true),
                    GrammaticId = table.Column<Guid>(nullable: false),
                    ToLanguage = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTranslates", x => x.HistoryTranslateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTranslates");
        }
    }
}
