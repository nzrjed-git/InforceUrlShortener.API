using AutoMapper;

namespace InforceUrlShortener.Application.User.DTOs
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CurrentUser, UserInfoDto>();
        }
    }
}
