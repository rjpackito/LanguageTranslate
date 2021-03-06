﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LanguageTranslate.Data;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    [DbContext(typeof(LanguageTranslateContext))]
    [Migration("20170517211818_add-fieldDateToHistoryTranslate")]
    partial class addfieldDateToHistoryTranslate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LanguageTranslate.Data.DbModels.GeneratedDLLs", b =>
                {
                    b.Property<Guid>("GeneratedDLLId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GrammaticId");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Title");

                    b.HasKey("GeneratedDLLId");

                    b.ToTable("GeneratedDLLs");
                });

            modelBuilder.Entity("LanguageTranslate.Data.DbModels.Grammatics", b =>
                {
                    b.Property<Guid>("GrammaticId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CreateUserId");

                    b.Property<bool>("IsEdit");

                    b.Property<bool>("IsValidate");

                    b.Property<DateTime>("LastDateEdit");

                    b.Property<Guid>("LastUserEditId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("GrammaticId");

                    b.ToTable("Grammatics");
                });

            modelBuilder.Entity("LanguageTranslate.Data.DbModels.HistoryTranslates", b =>
                {
                    b.Property<Guid>("HistoryTranslateId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTranslate");

                    b.Property<string>("FromLanguage");

                    b.Property<Guid>("GrammaticId");

                    b.Property<string>("ToLanguage");

                    b.Property<Guid>("UserId");

                    b.HasKey("HistoryTranslateId");

                    b.ToTable("HistoryTranslates");
                });

            modelBuilder.Entity("LanguageTranslate.Data.DbModels.VerifiedGrammars", b =>
                {
                    b.Property<Guid>("VerifiedGrammarId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GrammaticId");

                    b.Property<DateTime>("LastDateEdit");

                    b.Property<Guid>("LastUserEditId");

                    b.Property<string>("Path");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("VerifiedGrammarId");

                    b.ToTable("VerifiedGrammars");
                });
        }
    }
}
