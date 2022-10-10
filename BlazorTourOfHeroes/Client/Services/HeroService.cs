namespace BlazorTourOfHeroes.Client.Services;

public class HeroService
{
    private readonly HttpClient _httpClient;
    private readonly MessageService _messageService;

    public HeroService(HttpClient httpClient, MessageService messageService)
    {
        _httpClient = httpClient;
        _messageService = messageService;
    }

    private const string HeroesApiUrl = "/heroes";

    public async Task<IEnumerable<Hero>?> GetHeroes()
    {
        Log(nameof(GetHeroes));
        return await _httpClient.GetFromJsonAsync<Hero[]>(HeroesApiUrl);
    }

    public async Task<Hero?> GetHero(long id)
    {
        Log(nameof(GetHero));
        return await _httpClient.GetFromJsonAsync<Hero>($"{HeroesApiUrl}/{id}");
    }

    public async Task<Hero?> UpdateHero(Hero hero)
    {
        Log(nameof(UpdateHero));
        var response = await _httpClient.PutAsJsonAsync(HeroesApiUrl, hero);
        return await response.Content.ReadFromJsonAsync<Hero>();
    }

    public async Task<Hero?> AddHero(Hero hero)
    {
        Log(nameof(AddHero));
        var response = await _httpClient.PostAsJsonAsync(HeroesApiUrl, hero);
        return await response.Content.ReadFromJsonAsync<Hero>();
    }

    public async Task DeleteHero(long id)
    {
        Log(nameof(DeleteHero));
        await _httpClient.DeleteAsync($"{HeroesApiUrl}/{id}");
    }

    public async Task<IEnumerable<Hero>?> SearchHeroes(string term)
    {
        Log(nameof(SearchHeroes));
        return string.IsNullOrWhiteSpace(term)
            ? Array.Empty<Hero>()
            : (IEnumerable<Hero>?)await _httpClient.GetFromJsonAsync<Hero[]>(
                $@"{HeroesApiUrl}/search?searchTerms={term}");
    }

    private void Log(string message) => _messageService.Add($"HeroService: {message}");
}
