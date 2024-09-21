using System.ComponentModel.DataAnnotations;

namespace ssptb.pe.tdlt.user.entities.Entities;
public class User
{
    [Key]
    public Guid UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string HashedPassword { get; set; } = string.Empty;

    public string SaltPassword { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string JobTitle { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public string AccountStatus { get; set; } = string.Empty;

    // Foreign Key to Role
    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    // Relationship to UserActivityLog (1 to Many)
    public List<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();
}

public class UserPermission
{
    [Key]
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

public class UserActivityLog
{
    [Key]
    public Guid ActivityLogId { get; set; }

    public Guid UserId { get; set; }

    public string ActivityType { get; set; } = string.Empty;

    public DateTime ActivityDate { get; set; }

    public string IPAddress { get; set; } = string.Empty;
}

public class Role
{
    [Key]
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Relationship to User (1 to Many)
    public List<User> Users { get; set; } = new List<User>();

    // Relationship to RolePermission (Many to Many through RolePermission)
    public List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

public class RolePermission
{
    [Key]
    public Guid RolePermissionId { get; set; }

    // Foreign Key to Role
    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    // Foreign Key to UserPermission
    public Guid PermissionId { get; set; }

    public UserPermission Permission { get; set; }
}