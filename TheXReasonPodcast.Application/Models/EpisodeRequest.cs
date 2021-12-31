using System;

namespace TheXReasonPodcast.Application.Models
{
    public class EpisodeRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Guest { get; set; }
        public string LiveLink { get; set; }
        public DateTime? StartStreaming { get; set; }
        public DateTime? StopStreaming { get; set; }
    }
}