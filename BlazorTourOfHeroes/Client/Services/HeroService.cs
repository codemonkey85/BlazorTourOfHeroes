namespace BlazorTourOfHeroes.Client.Services;

public record HeroService(HttpClient HttpClient, MessageService MessageService)
{
    private const string HeroesApiUrl = "/api/heroes";

    public async Task<IEnumerable<Hero>?> GetHeroes()
    {
        Log(nameof(GetHeroes));
        return await HttpClient.GetFromJsonAsync<Hero[]>(HeroesApiUrl);
    }

    public async Task<Hero?> GetHero(long id)
    {
        Log(nameof(GetHero));
        return await HttpClient.GetFromJsonAsync<Hero>($"{HeroesApiUrl}/{id}");
    }

    public async Task<Hero?> UpdateHero(Hero hero)
    {
        Log(nameof(UpdateHero));
        var response = await HttpClient.PutAsJsonAsync(HeroesApiUrl, hero);
        return await response.Content.ReadFromJsonAsync<Hero>();
    }

    public async Task<Hero?> AddHero(Hero hero)
    {
        Log(nameof(AddHero));
        var response = await HttpClient.PostAsJsonAsync(HeroesApiUrl, hero);
        return await response.Content.ReadFromJsonAsync<Hero>();
    }

    public async Task DeleteHero(long id)
    {
        Log(nameof(DeleteHero));
        await HttpClient.DeleteAsync($"{HeroesApiUrl}/{id}");
    }

    public async Task<IEnumerable<Hero>?> SearchHeroes(string term)
    {
        Log(nameof(SearchHeroes));
        return string.IsNullOrWhiteSpace(term)
            ? Array.Empty<Hero>()
            : (IEnumerable<Hero>?)await HttpClient.GetFromJsonAsync<Hero[]>(
                $@"{HeroesApiUrl}/search?searchTerms={term}");
    }

    private void Log(string message) => MessageService.Add($"HeroService: {message}");
}
