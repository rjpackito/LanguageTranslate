using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    public partial class Verifiedg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "VerifiedGrammars",
                columns: table => new
                {
                    VerifiedGrammarId = table.Column<Guid>(nullable: false),
                    GrammaticId = table.Column<Guid>(nullable: false),
                    LastDateEdit = table.Column<DateTime>(nullable: false),
                    LastUserEditId = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifiedGrammars", x => x.VerifiedGrammarId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerifiedGrammars");

        }
    }
}
