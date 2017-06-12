using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    public partial class renameconveyor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrammaticId",
                table: "HistoryTranslates");

            migrationBuilder.AddColumn<string>(
                name: "Сonveyor",
                table: "HistoryTranslates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Сonveyor",
                table: "HistoryTranslates");

            migrationBuilder.AddColumn<Guid>(
                name: "GrammaticId",
                table: "HistoryTranslates",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
