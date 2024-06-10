using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DBContext
{
    public class FinalDBContext : DbContext
    {
        public FinalDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ogretmenler> Ogretmenler { get; set; }
        public DbSet<Dersler> Dersler { get; set; }
        public DbSet<Ders> Ders { get; set; }
        public DbSet<Ogrenciler> Ogrenciler { get; set; }
        public DbSet<Notlar> Not { get; set; }
        public DbSet<Duyurular>? Duyurular { get; set; }
        public DbSet<Belgeler>? Belgeler { get; set; }
        public DbSet<Yerler>? Yerlers { get; set; }
        public DbSet<UserProfile>? UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ogretmenler ve Ders arasındaki ilişkiyi tanımlama (Many-to-One)
            modelBuilder.Entity<Ogretmenler>()
                .HasOne(o => o.DersAd)
                .WithMany()
                .HasForeignKey(o => o.DersId)
                .OnDelete(DeleteBehavior.NoAction);

            // Dersler ve Ders arasındaki ilişkiyi tanımlama (Many-to-One)
            modelBuilder.Entity<Dersler>()
                .HasOne(d => d.Ders)
                .WithMany()
                .HasForeignKey(d => d.DersId)
                .OnDelete(DeleteBehavior.NoAction);

            // Dersler ve Ogretmenler arasındaki ilişkiyi tanımlama (Many-to-One)
            modelBuilder.Entity<Dersler>()
                .HasOne(d => d.Ogretmen)
                .WithMany()
                .HasForeignKey(d => d.OgretmenId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ogrenciler>()
                .HasOne(o => o.Sınıf)
                .WithMany()
                .HasForeignKey(o => o.SınıfId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
