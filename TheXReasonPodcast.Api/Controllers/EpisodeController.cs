using Microsoft.AspNetCore.Mvc;
using System;
using TheXReasonPodcast.Application.Models;
using TheXReasonPodcast.Domain.Entities;
using TheXReasonPodcast.Infrastructure.Repositories;

namespace TheXReasonPodcast.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeController(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        //TODO - Install AutoMapper
        [HttpPost]
        [Route("insert")]
        public IActionResult Post([FromBody] EpisodeRequest episodeRequest)
        {
            var episodeEntity = new EpisodeEntity()
            {
                Id = episodeRequest.Id,
                Title = episodeRequest.Title,
                Guest = episodeRequest.Guest,
                LiveLink = episodeRequest.LiveLink,
                StartStreaming = null,
                StopStreaming = null
            };

            _episodeRepository.InsertEpisode(episodeEntity);

            return CreatedAtAction(nameof(Get), new { id = episodeEntity.Id }, episodeEntity);
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            var response = _episodeRepository.GetAllEpisodes();

            return Ok(response);
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id)
        {
            var response = _episodeRepository.GetEpisode(id);

            IActionResult result = response != null ?
                Ok(response) :
                NotFound();

            return result;
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Put([FromBody] EpisodeRequest episodeRequest)
        {
            var episodeEntity = _episodeRepository.GetEpisode(episodeRequest.Id);

            if (episodeEntity != null)
            {
                episodeEntity.Title = episodeRequest.Title;
                episodeEntity.Guest = episodeRequest.Guest;
                episodeEntity.LiveLink = episodeRequest.LiveLink;
                episodeEntity.StartStreaming = null;
                episodeEntity.StopStreaming = null;

                _episodeRepository.UpdateEpisode(episodeEntity);

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}