using MediatR;
using ssptb.pe.tdlt.user.common.Responses;

namespace ssptb.pe.tdlt.user.command.Command;
public class DeleteUserCommand : IRequest<ApiResponse<string>>
{
    public Guid UserId { get; }

    public DeleteUserCommand(Guid userId)
    {
        UserId = userId;
    }
}