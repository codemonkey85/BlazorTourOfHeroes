namespace BlazorTourOfHeroes.Server.Api;

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

    private async static Task<IResult> GetHeroes(HttpRequest request, [FromServices] AppDbContext dbContext)
    {
        return Results.Ok(await dbContext.Heroes.ToArrayAsync());
    }

    private async static Task<IResult> GetHero(HttpRequest request, long id, [FromServices] AppDbContext dbContext)
    {
        return Results.Ok(await dbContext.Heroes.FindAsync(id));
    }

    private async static Task<IResult> CreateHero(HttpRequest request, Hero hero, [FromServices] AppDbContext dbContext)
    {
        dbContext.Add(hero);
        await dbContext.SaveChangesAsync();
        return Results.Ok(hero);
    }

    private async static Task<IResult> UpdateHero(HttpRequest request, Hero hero, [FromServices] AppDbContext dbContext)
    {
        dbContext.Entry(hero).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return Results.Ok(hero);
    }

    private async static Task<IResult> DeleteHero(HttpRequest request, int id, [FromServices] AppDbContext dbContext)
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
}
