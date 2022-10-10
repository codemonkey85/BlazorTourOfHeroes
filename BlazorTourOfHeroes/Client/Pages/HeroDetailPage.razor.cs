namespace BlazorTourOfHeroes.Client.Pages;

public partial class HeroDetailPage
{
    [Parameter]
    public long Id { get; set; }

    private Hero? _hero;

    async protected override Task OnParametersSetAsync() => _hero = await HeroService.GetHero(Id);
}
