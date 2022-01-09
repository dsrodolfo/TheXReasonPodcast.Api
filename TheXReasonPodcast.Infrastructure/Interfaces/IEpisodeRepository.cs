using System.Collections.Generic;
using TheXReasonPodcast.Domain.Entities;

namespace TheXReasonPodcast.Infrastructure.Interfaces
{
    public interface IEpisodeRepository
    {
        void DeleteEpisode(string id);
        IEnumerable<EpisodeEntity> GetAllEpisodes();
        EpisodeEntity GetEpisode(string id);
        void InsertEpisode(EpisodeEntity episodeEntity);
        void UpdateEpisode(EpisodeEntity episodeEntity);
    }
}