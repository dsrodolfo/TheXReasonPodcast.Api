﻿using System;

namespace TheXReasonPodcast.Domain.Entities
{
    public class EpisodeEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Guest { get; set; }
        public string LiveLink { get; set; }
        public DateTime? StartStreaming { get; set; }
        public DateTime? StopStreaming { get; set; }
    }
}