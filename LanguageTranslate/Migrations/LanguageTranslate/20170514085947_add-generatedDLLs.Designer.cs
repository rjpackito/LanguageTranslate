using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LanguageTranslate.Data;

namespace LanguageTranslate.Migrations.LanguageTranslate
{
    [DbContext(typeof(LanguageTranslateContext))]
    [Migration("20170514085947_add-generatedDLLs")]
    partial class addgeneratedDLLs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
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
