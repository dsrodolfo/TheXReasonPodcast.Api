using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheXReasonPodcast.Application.Models;
using TheXReasonPodcast.Domain.Entities;
using TheXReasonPodcast.Infrastructure.Interfaces;

namespace TheXReasonPodcast.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEpisodeRepository _episodeRepository;
        public EpisodeController(IMapper mapper, IEpisodeRepository episodeRepository)
        {
            _mapper = mapper;
            _episodeRepository = episodeRepository;
        }

        //TODO - Install AutoMapper
        [HttpPost]
        [Route("insert")]
        public IActionResult Post([FromBody] EpisodeRequest episodeRequest)
        {
            var episodeEntity = _mapper.Map<EpisodeEntity>(episodeRequest);

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
                episodeEntity.StartStreaming = episodeRequest.StartStreaming;
                episodeEntity.StopStreaming = episodeRequest.StopStreaming;

                _episodeRepository.UpdateEpisode(episodeEntity);

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var episodeEntity = _episodeRepository.GetEpisode(id);

            if (episodeEntity != null)
            {
                _episodeRepository.DeleteEpisode(id);

                return NoContent();
            }

            return NotFound();
        }
    }
}