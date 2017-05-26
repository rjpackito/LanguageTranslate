using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    public partial class addfieldToFromLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromLanguage",
                table: "VerifiedGrammars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToLanguage",
                table: "VerifiedGrammars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromLanguage",
                table: "Grammatics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToLanguage",
                table: "Grammatics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromLanguage",
                table: "GeneratedDLLs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToLanguage",
                table: "GeneratedDLLs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromLanguage",
                table: "VerifiedGrammars");

            migrationBuilder.DropColumn(
                name: "ToLanguage",
                table: "VerifiedGrammars");

            migrationBuilder.DropColumn(
                name: "FromLanguage",
                table: "Grammatics");

            migrationBuilder.DropColumn(
                name: "ToLanguage",
                table: "Grammatics");

            migrationBuilder.DropColumn(
                name: "FromLanguage",
                table: "GeneratedDLLs");

            migrationBuilder.DropColumn(
                name: "ToLanguage",
                table: "GeneratedDLLs");
        }
    }
}
