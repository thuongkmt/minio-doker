using Microsoft.EntityFrameworkCore;
using MinIOService.Domain.Models;

namespace MinIOService.Infrastructure
{
    public class MinIOServiceDBContext : DbContext
    {
        public MinIOServiceDBContext()
        {

        }
        public MinIOServiceDBContext(DbContextOptions<MinIOServiceDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(10,4,15));
            var connectionString = "server=localhost;user=npm;password=npm;database=npm";
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UploadEntity>()
                .HasKey(uploadModel => new { uploadModel.Id });

            modelBuilder.Entity<UploadEntity>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
        }
        public DbSet<UploadEntity> Uploads { set; get; }
    }
}

    
