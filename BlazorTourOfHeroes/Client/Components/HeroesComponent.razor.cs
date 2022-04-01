namespace BlazorTourOfHeroes.Client.Components;

public partial class HeroesComponent
{
    private IList<Hero> heroes = new List<Hero>();
    private readonly Hero? selectedHero;
    private string heroName = string.Empty;
    async protected override Task OnInitializedAsync() => await GetHeroes();

    private async Task GetHeroes() => heroes = (await HeroService.GetHeroes())?.ToList() ?? new List<Hero>();

    private async void Add()
    {
        var addedHero = await HeroService.AddHero(new Hero { Name = heroName.Trim() });
        if (addedHero is not null)
        {
            heroes.Add(addedHero);
            heroName = string.Empty;
        }
    }

    private async Task Delete(Hero hero)
    {
        await HeroService.DeleteHero(hero.Id);
        heroes.Remove(hero);
    }
}
