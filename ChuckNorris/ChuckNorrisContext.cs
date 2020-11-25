using Microsoft.EntityFrameworkCore;

#pragma warning disable 8618

namespace ChuckNorris {
    public class ChuckNorrisContext : DbContext {
        public ChuckNorrisContext(DbContextOptions<ChuckNorrisContext> options) : base(options) { }

        public DbSet<ChuckNorrisJoke> Jokes { get; set; }
    }
}

