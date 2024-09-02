using MediatR;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.command.Queries;
public class GetUserByIdQuery : IRequest<ApiResponse<GetUserByIdResponse>>
{
    public Guid UserId { get; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}