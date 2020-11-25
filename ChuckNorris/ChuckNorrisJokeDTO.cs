using System;
// ReSharper disable InconsistentNaming

namespace ChuckNorris {
    public class ChuckNorrisJokeDto {
        public string[] categories = Array.Empty<string>();
        public string id { get; set; } = "";
        public string url { get; set; } = "";
        public string iconUrl { get; set; } = "";
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string value { get; set; } = "";
    }
}
