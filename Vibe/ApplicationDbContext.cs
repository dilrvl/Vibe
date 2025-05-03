using Microsoft.EntityFrameworkCore;
using System;
using Vibe.Core.Entities;

namespace Vibe.Forms
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// класс с подключением к бд
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<TrackRating> TrackRatings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vibe.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
