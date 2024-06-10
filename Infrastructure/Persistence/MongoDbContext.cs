using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

public class MongoDbContext: DbContext
{
    
    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }

    public DbSet<FileApp> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FileApp>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Size).IsRequired();
            entity.Property(e => e.CreatedDate).IsRequired();
            entity.Property(e => e.ModifiedDate).IsRequired();
            entity.Property(e => e.uploadedBy).IsRequired();
        });
    }
}
