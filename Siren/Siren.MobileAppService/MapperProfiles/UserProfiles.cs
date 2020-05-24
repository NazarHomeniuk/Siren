using AutoMapper;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Map;

namespace Siren.MobileAppService.MapperProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserMapInfo>()
                .ForMember(u => u.TrackInfo, o => o.MapFrom(u => $"{u.Track.Artist} - {u.Track.Title}"))
                .ForMember(u => u.UserId, o => o.MapFrom(u => u.Id));
        }
    }
}
