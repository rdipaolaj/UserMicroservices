using MediatR;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.command.Queries;
public class GetUserByUsernameQuery : IRequest<ApiResponse<GetUserByUsernameResponse>>
{
    public string Username { get; }

    public GetUserByUsernameQuery(string username)
    {
        Username = username;
    }
}
