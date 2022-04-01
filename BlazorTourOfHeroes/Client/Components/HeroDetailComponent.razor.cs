using BlazorTourOfHeroes.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorTourOfHeroes.Client.Components;

public partial class HeroDetailComponent
{
    [Parameter]
    public Hero? Hero { get; set; }

    private async Task Save()
    {
        if (Hero is not null)
        {
            await HeroService.UpdateHero(Hero);
        }
    }
}
