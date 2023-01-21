using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeymanAkhtari.Models;

namespace PeymanAkhtari.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Feature> Features{ get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Content> Contents  { get; set; }

        public AppDbContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // call the base if you are using Identity service.
            // Important
            base.OnModelCreating(builder);

            // Code here ...
        }
    }
}
