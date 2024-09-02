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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<UserPermission>()
            .HasKey(p => p.PermissionId);

        modelBuilder.Entity<UserActivityLog>()
            .HasKey(a => a.ActivityLogId); 
    }
}
