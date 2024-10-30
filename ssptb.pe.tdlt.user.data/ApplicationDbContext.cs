using Microsoft.EntityFrameworkCore;
using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Definir DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<UserActivityLog> UserActivityLogs { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Especifica el esquema predeterminado
        modelBuilder.HasDefaultSchema("userdb");

        // Configurar relación User-Role
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        // Configurar relación RolePermission
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);

        // Configurar relación UserActivityLog
        modelBuilder.Entity<UserActivityLog>()
            .HasOne<User>()
            .WithMany(u => u.ActivityLogs)
            .HasForeignKey(ual => ual.UserId);
    }
}
