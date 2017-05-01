using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LanguageTranslate.Data;

namespace LanguageTranslate.Migrations
{
    [DbContext(typeof(LanguageTranslateContext))]
    partial class LanguageTranslateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("LanguageTranslate.Models.Grammatic", b =>
                {
                    b.Property<Guid>("GrammaticId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CreateUserId");

                    b.Property<string>("CreateUserTitle");

                    b.Property<bool>("IsEdit");

                    b.Property<bool>("IsValidate");

                    b.Property<DateTime>("LastDateEdit");

                    b.Property<Guid>("LastUserEditId");

                    b.Property<string>("LastUserEditTitle");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("GrammaticId");

                    b.ToTable("Grammatic");
                });
        }
    }
}
