using Microsoft.EntityFrameworkCore;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MovieAppearanceEntity> MovieAppearances { get; set; }
    }
}