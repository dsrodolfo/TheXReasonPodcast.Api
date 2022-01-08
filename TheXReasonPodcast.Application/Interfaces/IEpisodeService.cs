using System.Collections.Generic;
using TheXReasonPodcast.Application.Models;
using TheXReasonPodcast.Domain.Entities;

namespace TheXReasonPodcast.Application.Interfaces
{
    public interface IEpisodeService
    {
        void DeleteEpisode(int id);
        IEnumerable<EpisodeEntity> GetAllEpisodes();
        EpisodeEntity GetEpisode(int id);
        int InsertEpisode(EpisodeRequest episodeRequest);
        bool UpdateEpisode(EpisodeRequest episodeRequest);
    }
}