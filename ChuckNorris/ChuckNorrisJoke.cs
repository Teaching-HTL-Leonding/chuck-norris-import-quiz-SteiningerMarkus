using System.ComponentModel.DataAnnotations;

namespace ChuckNorris {
    public class ChuckNorrisJoke {
        public int Id { get; set; }

        [MaxLength(40)]
        public string ChuckNorrisId { get; set; } = "";

        [MaxLength(1024)]
        public string Url { get; set; } = "";

        public string Joke { get; set; } = "";
    }
}
