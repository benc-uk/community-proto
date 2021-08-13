using Microsoft.EntityFrameworkCore;
using CommunityApi.Models;

namespace CommunityApi.Data
{
    public class CommunityDbContext : DbContext
    {
        public CommunityDbContext(DbContextOptions<CommunityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>()
                .HasMany(c => c.Discussions);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Community> Communities { get; set; }
    }
}