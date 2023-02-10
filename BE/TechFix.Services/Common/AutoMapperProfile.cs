using AutoMapper;
using TechFix.EntityModels;
using TechFix.TransportModels;
using TechFix.TransportModels.Auth;

namespace TechFix.Services.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoginTransport, User>();
            CreateMap<ProfileTransport, User>();
            CreateMap<User, ProfileTransport>();
        }
    }
}
