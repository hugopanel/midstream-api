using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.User;
using Domain.User.ValueObjects;

namespace Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberRole> MemberRole { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Password, pw =>
                {
                    pw.Property(p => p.HashedPassword).HasColumnName("PasswordHash").UsePropertyAccessMode(PropertyAccessMode.Field);
                });
            });

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<MemberRole>(entity =>
            {
                entity.HasKey(mr => new { mr.MemberId, mr.RoleId });
            });
                

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever(); // We prefer to generate the GUID values in the application instead of the database
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            /* modelBuilder.Entity<Member>()
                .HasMany(m => m.Roles)
                .WithMany(r => r.Members)
                .UsingEntity<Dictionary<string, object>>(
                    "MemberRole",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<Member>().WithMany().HasForeignKey("MemberId"));

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Project)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
