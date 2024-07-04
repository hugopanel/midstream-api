using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Domain.User;
using Domain.User.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // public DbSet<Permission> Permission { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Role> Role { get; set; }
        // public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberRole> MemberRole { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var permissionsConverter = new PermissionsValueConverter();

            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Password, pw =>
                {
                    pw.Property(p => p.HashedPassword).HasColumnName("PasswordHash").UsePropertyAccessMode(PropertyAccessMode.Field);
                });
            });

            // modelBuilder.Entity<RolePermission>()
            //     // .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            //     .HasKey(rp => new { rp.RoleId, rp.PermissionCode });
            //     // .HasKey(rp => new { rp.Role, rp.Permission });
            
            modelBuilder.Ignore<Permission>();

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

            // var permissionsConverter = new ValueConverter<List<Permission>, List<string>>(
            //     permissions => permissions.Select(p => p.Code).ToList(),
            //     codes => codes.Select(code => PermissionMapper.Permissions.FirstOrDefault(p => p.Code == code)).ToList()!);
            // var permissionsConverter = new ValueConverter<Permission, string>(
            //     permission => permission.Code,
            //     code => PermissionMapper.Permissions.FirstOrDefault(p => p.Code == code)!
            // );
            
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                // entity.HasMany(r => r.Permissions).WithMany("Roles").UsingEntity<Dictionary<string, object>>(
                //     "RolePermission",
                //     j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionCode"),
                //     j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                //     j =>
                //     {
                //         j.Property<string>("PermissionCode");
                //         j.Property<Guid>("RoleId");
                //         j.HasKey("PermissionCode", "RoleId");
                //     });

                // entity.Property(e => e.Id).ValueGeneratedNever();
                // entity.Property(e => e.Name).IsRequired();
                
                // entity.Property(e => e.Permissions)
                //     .HasConversion(permissionsConverter)
                //     .HasPostgresArrayConversion<Permission, string>(permissionsConverter)
                //     .Metadata.SetValueConverter(permissionsConverter);
                
                entity.Property(e => e.Permissions)
                    .HasConversion(permissionsConverter)
                    // .HasPostgresArrayConversion<Permission, string>(permissionsConverter) // Ensure this line correctly applies the converter for a list
                    .Metadata.SetValueConverter(permissionsConverter);
            });

            // modelBuilder.Entity<Permission>(entity =>
            // {
            //     entity.HasKey(p => p.Code);
            // });

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
