namespace BlazorTourOfHeroes.Server.Api;

public static class HeroesApi
{
    public static IEndpointRouteBuilder MapHeroesApi(this IEndpointRouteBuilder apiGroup)
    {
        var herosGroup = apiGroup.MapGroup("heroes");

        herosGroup.MapGet("/", GetHeroes);
        herosGroup.MapGet("/{id}", GetHero);
        herosGroup.MapPost("/", CreateHero);
        herosGroup.MapPut("/", UpdateHero);
        herosGroup.MapDelete("/{id}", DeleteHero);
        herosGroup.MapGet("/search", SearchHeroes);

        return apiGroup;
    }

    private async static Task<IResult> GetHeroes(HttpRequest request, AppDbContext dbContext) =>
        Results.Ok(await dbContext.Heroes.ToArrayAsync());

    private async static Task<IResult> GetHero(HttpRequest request, long id, AppDbContext dbContext) =>
        Results.Ok(await dbContext.Heroes.FindAsync(id));

    private async static Task<IResult> CreateHero(HttpRequest request, Hero hero, AppDbContext dbContext)
    {
        dbContext.Add(hero);
        await dbContext.SaveChangesAsync();
        return Results.Ok(hero);
    }

    private async static Task<IResult> UpdateHero(HttpRequest request, Hero hero, AppDbContext dbContext)
    {
        dbContext.Entry(hero).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return Results.Ok(hero);
    }

    private async static Task<IResult> DeleteHero(HttpRequest request, int id, AppDbContext dbContext)
    {
        var hero = await dbContext.Heroes.FindAsync(id);
        if (hero is null)
        {
            return Results.NotFound();
        }

        dbContext.Entry(hero).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    private async static Task<IResult> SearchHeroes(HttpRequest request, [FromQuery] string searchTerms,
        AppDbContext dbContext) =>
        Results.Ok(string.IsNullOrWhiteSpace(searchTerms)
            ? []
            : await (
                from heroes in dbContext.Heroes
                where EF.Functions.Like(heroes.Name, $"%{searchTerms}%")
                select heroes
            ).ToArrayAsync());
}
