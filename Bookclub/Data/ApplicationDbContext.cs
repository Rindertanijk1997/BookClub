using Bookclub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookclub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ClubBook> ClubBooks { get; set; }
        public DbSet<Discussion> Discussions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(string) && property.GetMaxLength() == null)
                    {
                        property.SetMaxLength(256);
                    }
                }
            }
        }
    }
}