using liriksi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.EF
{
    public partial class LiriksiContext : DbContext
    {
        public LiriksiContext()
        {
        }
        public LiriksiContext(DbContextOptions<LiriksiContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Song> Song { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Performer> Performer { get; set; }
        public DbSet<UsersAlbumRate> UsersAlbumRates { get; set; }
        public DbSet<UsersSongRate> UsersSongRates { get; set; }
        public DbSet<Genre> Genre { get; set; }

        //kazemo mu da je ovo kompozicija
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersAlbumRate>().HasKey(ba => new { ba.AlbumId, ba.UserId });
            modelBuilder.Entity<UsersSongRate>().HasKey(ba => new { ba.SongId, ba.UserId });
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-CP7742G; Database=liriksiDB; Trusted_Connection=true;"); 
        //}
    }
}
