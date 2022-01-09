using AutoMapper;
using MongoDB.Bson;
using System.Collections.Generic;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Models.Requests;
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

        public string InsertEpisode(EpisodeRequest episodeRequest)
        {
            var episodeEntity = _mapper.Map<EpisodeEntity>(episodeRequest);
            episodeEntity.Id = ObjectId.GenerateNewId().ToString();

            _episodeRepository.InsertEpisode(episodeEntity);

            return episodeEntity.Id;
        }

        public IEnumerable<EpisodeEntity> GetAllEpisodes()
        {
            return _episodeRepository.GetAllEpisodes();
        }

        public EpisodeEntity GetEpisode(string id)
        {
            var episodeEntity = _episodeRepository.GetEpisode(id);

            return episodeEntity;
        }

        public bool UpdateEpisode(EpisodeUpdateRequest episodeUpdateRequest)
        {
            var episodeEntity = _episodeRepository.GetEpisode(episodeUpdateRequest.Id);

            if (episodeEntity != null)
            {
                var episodeUpdateEntity = _mapper.Map<EpisodeEntity>(episodeUpdateRequest);
                episodeUpdateEntity.Id = episodeEntity.Id;

                _episodeRepository.UpdateEpisode(episodeUpdateEntity);

                return true;
            }

            return false;
        }

        public void DeleteEpisode(string id)
        {
            _episodeRepository.DeleteEpisode(id);
        }
    }
}