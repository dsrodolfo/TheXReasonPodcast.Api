using System.Collections.Generic;
using TheXReasonPodcast.Application.Models.Requests;
using TheXReasonPodcast.Domain.Entities;

namespace TheXReasonPodcast.Application.Interfaces
{
    public interface IEpisodeService
    {
        void DeleteEpisode(string id);
        IEnumerable<EpisodeEntity> GetAllEpisodes();
        EpisodeEntity GetEpisode(string id);
        string InsertEpisode(EpisodeRequest episodeRequest);
        bool UpdateEpisode(EpisodeUpdateRequest episodeUpdateRequest);
    }
}