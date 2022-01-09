using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Models.Requests;

namespace TheXReasonPodcast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpPost]
        [Route("insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] EpisodeRequest episodeRequest)
        {
            var episodeId = _episodeService.InsertEpisode(episodeRequest);

            return CreatedAtAction(nameof(Get), new { id = episodeId }, episodeRequest);
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var response = _episodeService.GetAllEpisodes();

            return Ok(response);
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string id)
        {
            var response = _episodeService.GetEpisode(id);
            IActionResult result = response != null ? Ok(response) : NotFound();

            return result;
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromBody] EpisodeUpdateRequest episodeUpdateRequest)
        {
            var isEpisodeUpdated = _episodeService.UpdateEpisode(episodeUpdateRequest);
            IActionResult result = isEpisodeUpdated ? NoContent() : NotFound();

            return result;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            var episodeEntity = _episodeService.GetEpisode(id);

            if (episodeEntity != null)
            {
                _episodeService.DeleteEpisode(id);

                return NoContent();
            }

            return NotFound();
        }
    }
}