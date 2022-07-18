using Microsoft.EntityFrameworkCore;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CharacterEntity> Characters { get; set; }

        public DbSet<MovieAppearanceEntity> MovieAppearances { get; set; }

        public DbSet<MoviesEntity> Movies { get; set ; }

        public DbSet<TVShowsEntity> TVShows { get; set; }

        public DbSet<TVShowAppearanceEntity> TVShowAppearance { get; set; }
    }
}