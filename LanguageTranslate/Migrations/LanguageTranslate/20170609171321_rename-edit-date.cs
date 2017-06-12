using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    public partial class renameeditdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastDateEdit",
                table: "VerifiedGrammars",
                newName: "EditDate");

            migrationBuilder.RenameColumn(
                name: "LastDateEdit",
                table: "Grammatics",
                newName: "EditDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditDate",
                table: "VerifiedGrammars",
                newName: "LastDateEdit");

            migrationBuilder.RenameColumn(
                name: "EditDate",
                table: "Grammatics",
                newName: "LastDateEdit");
        }
    }
}
