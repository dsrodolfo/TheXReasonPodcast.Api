﻿using MongoDB.Driver;
using System.Collections.Generic;
using TheXReasonPodcast.Domain.Entities;
using TheXReasonPodcast.Infrastructure.Interfaces;

namespace TheXReasonPodcast.Infrastructure.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly IMongoCollection<EpisodeEntity> _episodes;

        public EpisodeRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _episodes = database.GetCollection<EpisodeEntity>("episodes");
        }

        public void InsertEpisode(EpisodeEntity episodeEntity)
        {
            _episodes.InsertOne(episodeEntity);
        }

        public IEnumerable<EpisodeEntity> GetAllEpisodes()
        {
            return _episodes.Find(episode => true).ToList();           
        }

        public EpisodeEntity GetEpisode(string id)
        {
            return _episodes.Find(ep => ep.Id == id).FirstOrDefault();
        }

        public void UpdateEpisode(EpisodeEntity episodeEntity)
        {
            _episodes.ReplaceOne(ep => ep.Id == episodeEntity.Id, episodeEntity);
        }

        public void DeleteEpisode(string id)
        {
            _episodes.DeleteOne(ep => ep.Id == id);
        }
    }
}