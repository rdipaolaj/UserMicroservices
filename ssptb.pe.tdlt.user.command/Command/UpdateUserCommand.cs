using MediatR;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.command.Command;
public class UpdateUserCommand : IRequest<ApiResponse<UpdateUserResponse>>
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
}
