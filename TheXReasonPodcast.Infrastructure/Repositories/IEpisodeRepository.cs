using System.Collections.Generic;
using TheXReasonPodcast.Domain.Entities;

namespace TheXReasonPodcast.Infrastructure.Repositories
{
    public interface IEpisodeRepository
    {
        void DeleteEpisode(int id);
        IEnumerable<EpisodeEntity> GetAllEpisodes();
        EpisodeEntity GetEpisode(int id);
        void InsertEpisode(EpisodeEntity episodeEntity);
        void UpdateEpisode(EpisodeEntity episodeEntity);
    }
}