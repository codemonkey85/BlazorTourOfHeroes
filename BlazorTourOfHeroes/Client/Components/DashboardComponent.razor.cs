namespace BlazorTourOfHeroes.Client.Components;

public partial class DashboardComponent
{
    private IEnumerable<Hero> heroes = new List<Hero>();
    
    async protected override Task OnInitializedAsync() => await GetHeroes();

    private async Task GetHeroes() => heroes = (await HeroService.GetHeroes() ?? Array.Empty<Hero>()).Take(4);
}
