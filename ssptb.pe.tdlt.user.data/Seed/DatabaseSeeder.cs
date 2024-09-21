using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.data.Seed;
public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;

    public DatabaseSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Obtener el rol Admin
        var adminRole = _context.Roles.FirstOrDefault(r => r.RoleName == "Admin");

        if (adminRole == null)
        {
            // Crear rol Admin si no existe
            adminRole = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Admin",
                Description = "Administrator role"
            };
            _context.Roles.Add(adminRole);
            await _context.SaveChangesAsync();
        }

        // Verificar si existe el usuario Admin
        var adminUserExists = _context.Users.Any(u => u.Username == "admin");

        if (!adminUserExists)
        {
            // Crear el usuario Admin
            var adminUser = new User
            {
                UserId = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@domain.com",
                PhoneNumber = "123456789",
                HashedPassword = "ZEgcM8dXKwZ7bkcqw2fNRQ==", // Hash ficticio
                SaltPassword = "ae1KVd9bfxAGA7MtuBX0dQ==", // Salt ficticio
                CompanyName = "Admin Company",
                Department = "IT",
                JobTitle = "Administrator",
                CreatedAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                AccountStatus = "A",
                RoleId = adminRole.RoleId // Asignar el rol Admin
            };

            // Agregar el usuario admin a la base de datos
            _context.Users.Add(adminUser);
            await _context.SaveChangesAsync();
        }

        // Asignar permisos al rol Admin si no están asignados
        var allPermissions = _context.UserPermissions.ToList();

        foreach (var permission in allPermissions)
        {
            var exists = _context.RolePermissions
                .Any(rp => rp.RoleId == adminRole.RoleId && rp.PermissionId == permission.PermissionId);

            if (!exists)
            {
                var rolePermission = new RolePermission
                {
                    RolePermissionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    PermissionId = permission.PermissionId
                };

                _context.RolePermissions.Add(rolePermission);
            }
        }

        // Guardar cambios en la base de datos
        await _context.SaveChangesAsync();
    }
}
