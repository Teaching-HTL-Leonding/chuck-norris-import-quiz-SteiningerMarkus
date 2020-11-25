using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ChuckNorris;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

var factory = new ChuckNorrisContextFactory();
var dbContext = factory.CreateDbContext();
int count = 0;

if (args.Length == 0)
    count = 5;
else if (args[0] == "clear")
    await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM ChuckNorrisJoke");
else if (!int.TryParse(args[0], out count) || count > 10) {
    Console.Error.WriteLine("Invalid Argument");
    Environment.Exit(1);
}

HttpClient client = new();

await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

for (int i = 0; i < count; i++) {
    ChuckNorrisJoke chuckNorrisJoke;
    for (int j = 0;; j++) {
        if (j == 10) {
            Console.WriteLine("No new jokes");
            Environment.Exit(0);
        }

        string responseBody = await GetJokeAsync(client);
        ChuckNorrisJokeDto? jokeDto = JsonSerializer.Deserialize<ChuckNorrisJokeDto>(responseBody);
        chuckNorrisJoke = new() { ChuckNorrisId = jokeDto!.id, Url = jokeDto.url, Joke = jokeDto.value };

        if (!dbContext.Jokes.Contains(chuckNorrisJoke))
            break;
    }
    await dbContext.AddAsync(chuckNorrisJoke);
    await dbContext.SaveChangesAsync();
}

static async Task<string> GetJokeAsync(HttpClient client) {
    try {
        HttpResponseMessage response = await client.GetAsync("https://api.chucknorris.io/jokes/random");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    catch (HttpRequestException e) {
        Console.Error.WriteLine("Error: " + e.Message);
        Environment.Exit(2);
        return null;
    }
}
