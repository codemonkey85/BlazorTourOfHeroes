using BlazorTourOfHeroes.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorTourOfHeroes.Client.Pages;

public partial class HeroDetailPage
{
    [Parameter]
    public long Id { get; set; }

    private Hero? hero;
    async protected override Task OnParametersSetAsync() => hero = await HeroService.GetHero(Id);
}
