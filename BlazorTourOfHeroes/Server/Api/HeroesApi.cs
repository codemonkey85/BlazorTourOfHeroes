using BlazorTourOfHeroes.Shared.Models;

namespace BlazorTourOfHeroes.Server.Api
{
    public static class HeroesApi
    {
        public static void MapHeroesApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/heroes", GetHeroes);
            app.MapGet("/heroes/{id}", GetHero);
            app.MapPost("/heroes", CreateHero);
            app.MapPut("/heroes", UpdateHero);
            app.MapDelete("/heroes/{id}", DeleteHero);
        }

        private static readonly IList<Hero> heroes = new List<Hero>
        {
            new Hero { Id = 11, Name = "Dr Nice" },
            new Hero { Id = 12, Name = "Narco" },
            new Hero { Id = 13, Name = "Bombasto" },
            new Hero { Id = 14, Name = "Celeritas" },
            new Hero { Id = 15, Name = "Magneta" },
            new Hero { Id = 16, Name = "RubberMan" },
            new Hero { Id = 17, Name = "Dynama" },
            new Hero { Id = 18, Name = "Dr IQ" },
            new Hero { Id = 19, Name = "Magma" },
            new Hero { Id = 20, Name = "Tornado" },
        };

        private static async Task<IResult> GetHeroes(HttpRequest request)
        {
            return Results.Ok(heroes);
        }

        private static async Task<IResult> GetHero(HttpRequest request, long id)
        {
            return Results.Ok(heroes.FirstOrDefault(hero => hero.Id == id));
        }

        private static async Task<IResult> CreateHero(HttpRequest request, Hero hero)
        {
            var newHero = new Hero
            {
                Id = heroes.Max(h => h.Id) + 1,
                Name = hero.Name
            };
            heroes.Add(newHero);
            return Results.Ok(newHero);
        }

        private static async Task<IResult> UpdateHero(HttpRequest request, Hero hero)
        {
            var foundHero = heroes.FirstOrDefault(h => h.Id == hero.Id);
            if (foundHero is null)
            {
                return Results.NotFound();
            }
            var index = heroes.IndexOf(foundHero);
            heroes[index] = hero;
            return Results.Ok(hero);
        }

        private static async Task<IResult> DeleteHero(HttpRequest request, int id)
        {
            var foundHero = heroes.FirstOrDefault(h => h.Id == id);
            if (foundHero is not null)
            {
                heroes.Remove(foundHero);
            }
            return Results.Ok();
        }
    }
}
