using Microsoft.EntityFrameworkCore;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CharacterEntity> Characters { get; set; }

        public DbSet<MovieAppearanceEntity> MovieAppearances { get; set; }

        public DbSet<Movies> Movies { get; set ; }

        public DbSet<TVShows> TVShows { get; set; }
    }
}