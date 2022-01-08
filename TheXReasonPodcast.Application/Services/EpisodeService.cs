using AutoMapper;
using System.Collections.Generic;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Models;
using TheXReasonPodcast.Domain.Entities;
using TheXReasonPodcast.Infrastructure.Interfaces;

namespace TheXReasonPodcast.Application.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IMapper _mapper;
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeService(IMapper mapper, IEpisodeRepository episodeRepository)
        {
            _mapper = mapper;
            _episodeRepository = episodeRepository;
        }

        public int InsertEpisode(EpisodeRequest episodeRequest)
        {
            var episodeEntity = _mapper.Map<EpisodeEntity>(episodeRequest);
            _episodeRepository.InsertEpisode(episodeEntity);

            return episodeEntity.Id;
        }

        public IEnumerable<EpisodeEntity> GetAllEpisodes()
        {
            return _episodeRepository.GetAllEpisodes();
        }

        public EpisodeEntity GetEpisode(int id)
        {
            var episodeEntity = _episodeRepository.GetEpisode(id);

            return episodeEntity;
        }

        public bool UpdateEpisode(EpisodeRequest episodeRequest)
        {
            var episodeEntity = _episodeRepository.GetEpisode(episodeRequest.Id);

            if (episodeEntity != null)
            {
                episodeEntity.Title = episodeRequest.Title;
                episodeEntity.Guest = episodeRequest.Guest;
                episodeEntity.LiveLink = episodeRequest.LiveLink;
                episodeEntity.StartStreaming = episodeRequest.StartStreaming;
                episodeEntity.StopStreaming = episodeRequest.StopStreaming;

                _episodeRepository.UpdateEpisode(episodeEntity);

                return true;
            }

            return false;
        }

        public void DeleteEpisode(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}