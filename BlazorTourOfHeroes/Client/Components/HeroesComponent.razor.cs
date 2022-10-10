namespace BlazorTourOfHeroes.Client.Components;

public partial class HeroesComponent
{
    private IList<Hero> _heroes = new List<Hero>();

    // private readonly Hero? selectedHero;
    private string _heroName = string.Empty;

    async protected override Task OnInitializedAsync() => await GetHeroes();

    private async Task GetHeroes() => _heroes = (await HeroService.GetHeroes())?.ToList() ?? new List<Hero>();

    private async void Add()
    {
        var addedHero = await HeroService.AddHero(new Hero { Name = _heroName.Trim() });
        if (addedHero is null)
        {
            return;
        }

        _heroes.Add(addedHero);
        _heroName = string.Empty;
    }

    private async Task Delete(Hero hero)
    {
        await HeroService.DeleteHero(hero.Id);
        _heroes.Remove(hero);
    }
}
