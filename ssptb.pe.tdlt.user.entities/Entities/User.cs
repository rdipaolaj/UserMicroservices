using System.ComponentModel.DataAnnotations;

namespace ssptb.pe.tdlt.user.entities.Entities;
public class User
{
    [Key]
    public Guid UserId { get; set; }  // Identificador único del usuario
    public string Username { get; set; } = string.Empty;  // Nombre de usuario para login
    public string Email { get; set; } = string.Empty; // Correo electrónico para notificaciones y recuperación de cuenta
    public string PhoneNumber { get; set; } = string.Empty;  // Número de teléfono para autenticación multifactor
    public string HashedPassword { get; set; } = string.Empty;  // Contraseña encriptada
    public string SaltPassword { get; set; } = string.Empty;  // Salt de la contraseña
    public string UserRole { get; set; } = string.Empty;  // Rol del usuario (ejemplo: Admin, Auditor, etc.)
    public string CompanyName { get; set; } = string.Empty;  // Nombre de la empresa asociada al usuario
    public string Department { get; set; } = string.Empty;  // Departamento al que pertenece el usuario dentro de la empresa
    public string JobTitle { get; set; } = string.Empty;  // Título o puesto del usuario en la empresa
    public DateTime CreatedAt { get; set; }  // Fecha de creación de la cuenta
    public DateTime LastLogin { get; set; }  // Fecha y hora del último acceso
    public string AccountStatus { get; set; } = string.Empty;  // Estado de la cuenta (activo, bloqueado, etc.)
    public List<UserPermission> Permissions { get; set; } = new List<UserPermission>();  // Permisos específicos del usuario
    public List<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();  // Registro de actividades del usuario
}

public class UserPermission
{
    [Key]
    public Guid PermissionId { get; set; }  // Identificador único del permiso
    public string PermissionName { get; set; } = string.Empty;  // Nombre del permiso (ejemplo: ViewTransactions, ApproveTransactions)
    public string Description { get; set; } = string.Empty;  // Descripción del permiso
}

public class UserActivityLog
{
    [Key]
    public Guid ActivityLogId { get; set; }  // Identificador único del registro de actividad
    public Guid UserId { get; set; }  // Identificador del usuario asociado
    public string ActivityType { get; set; } = string.Empty;  // Tipo de actividad (ejemplo: Login, PasswordChange)
    public DateTime ActivityDate { get; set; }  // Fecha y hora de la actividad
    public string IPAddress { get; set; } = string.Empty;  // Dirección IP desde donde se realizó la actividad
}