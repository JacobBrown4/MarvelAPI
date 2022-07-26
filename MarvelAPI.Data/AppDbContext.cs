using Microsoft.EntityFrameworkCore;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<CharacterEntity> Characters { get; set; }

        public DbSet<MovieAppearanceEntity> MovieAppearances { get; set; }

        public DbSet<MoviesEntity> Movies { get; set; }

        public DbSet<TVShowsEntity> TVShows { get; set; }

        public DbSet<TVShowAppearanceEntity> TVShowAppearance { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }
        
        public DbSet<TeamMembershipEntity> TeamMemberships { get; set; }
    }
}