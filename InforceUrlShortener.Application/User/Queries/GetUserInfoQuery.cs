using InforceUrlShortener.Application.User.DTOs;
using MediatR;

namespace InforceUrlShortener.Application.User.Queries
{
    public class GetUserInfoQuery : IRequest<UserInfoDto>
    {
    }
}
