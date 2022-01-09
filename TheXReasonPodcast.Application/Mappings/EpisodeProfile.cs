using AutoMapper;
using TheXReasonPodcast.Application.Models.Requests;
using TheXReasonPodcast.Domain.Entities;

namespace TheXReasonPodcast.Application.Mappings
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<EpisodeRequest, EpisodeEntity>().ReverseMap();
            CreateMap<EpisodeUpdateRequest, EpisodeEntity>().ReverseMap();
        }
    }
}