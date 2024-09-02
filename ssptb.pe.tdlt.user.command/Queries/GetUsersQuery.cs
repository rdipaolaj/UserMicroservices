using MediatR;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.command.Queries;
public class GetUsersQuery : IRequest<ApiResponse<List<GetUserByIdResponse>>>
{
    // Puedes agregar filtros opcionales aquí, como nombre, rol, etc.
}
