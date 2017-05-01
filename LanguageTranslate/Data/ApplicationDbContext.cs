using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LanguageTranslate.Models;
using LanguageTranslate.Models.AccountViewModels;
using LanguageTranslate.Data.DbModels;

namespace LanguageTranslate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
    public class LanguageTranslateContext : DbContext
    {
        public DbSet<Grammatics> Grammatics { get; set; }
        public LanguageTranslateContext(DbContextOptions<LanguageTranslateContext> options)
            : base(options)
        {
        }
        public DbSet<Grammatic> Grammatic { get; set; }
    }
}
