using AutoMapper;
using InforceUrlShortener.Application.User.DTOs;
using MediatR;

namespace InforceUrlShortener.Application.User.Queries
{
    public class GetUserInfoQueryHandler(
        IUserContext userContext,
        IMapper mapper)
        : IRequestHandler<GetUserInfoQuery, UserInfoDto>
    {
        public Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            var userDto = mapper.Map<UserInfoDto>(currentUser);
            return Task.FromResult(userDto);
        }
    }
}
