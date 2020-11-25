using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChuckNorris {
    public class ChuckNorrisContextFactory : IDesignTimeDbContextFactory<ChuckNorrisContext> {
        public ChuckNorrisContext CreateDbContext(string[]? args = null) {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<ChuckNorrisContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new ChuckNorrisContext(optionsBuilder.Options);
        }
    }
}
