﻿namespace BlazorTourOfHeroes.Server.Data;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<Hero>().HasData(
        new Hero { Id = 11, Name = "Dr Nice" },
        new Hero { Id = 12, Name = "Narco" },
        new Hero { Id = 13, Name = "Bombasto" },
        new Hero { Id = 14, Name = "Celeritas" },
        new Hero { Id = 15, Name = "Magneta" },
        new Hero { Id = 16, Name = "RubberMan" },
        new Hero { Id = 17, Name = "Dynama" },
        new Hero { Id = 18, Name = "Dr IQ" },
        new Hero { Id = 19, Name = "Magma" },
        new Hero { Id = 20, Name = "Tornado" }
    );

    public virtual DbSet<Hero> Heroes { get; set; } = default!;
}
