using Microsoft.EntityFrameworkCore;

namespace MangaRepositoryAPI.Data
{
    public class MangaRepoDbContext : DbContext
    {
        public MangaRepoDbContext() : base() { }

        public MangaRepoDbContext(DbContextOptions<MangaRepoDbContext> options) : base(options) { }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Manga>()
                        .HasOne(m => m.Status)
                        .WithOne(s => s.Manga)
                        .HasForeignKey<Status>(s => s.MangaId);
            modelBuilder.Entity<Manga>()
                        .HasMany(m => m.Authors)
                        .WithMany(a => a.Mangas)
                        .UsingEntity(j => j.ToTable("Write"));
            modelBuilder.Entity<Manga>()
                        .HasMany(m => m.Genres)
                        .WithMany(g => g.Mangas)
                        .UsingEntity(j => j.ToTable("Belong"));
        }
    }
}
