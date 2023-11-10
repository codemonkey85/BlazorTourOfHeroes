namespace BlazorTourOfHeroes.Client.Components;

public partial class HeroesComponent
{
    private List<Hero> heroes = [];

    // private readonly Hero? selectedHero;
    private string heroName = string.Empty;

    async protected override Task OnInitializedAsync() => await GetHeroes();

    private async Task GetHeroes() => heroes = (await HeroService.GetHeroes())?.ToList() ?? [];

    private async Task Add()
    {
        var addedHero = await HeroService.AddHero(new Hero { Name = heroName.Trim() });
        if (addedHero is null)
        {
            return;
        }

        heroName = string.Empty;
        heroes.Add(addedHero);
    }

    private async Task Delete(Hero hero)
    {
        await HeroService.DeleteHero(hero.Id);
        heroes.Remove(hero);
    }
}
