var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllersWithViews();
services.AddRazorPages();

services.AddDbContextFactory<AppDbContext>(options => options.UseInMemoryDatabase(nameof(AppDbContext)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app
    .UseHttpsRedirection()

    .UseBlazorFrameworkFiles()
    .UseStaticFiles()

    .UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.EnsureCreated();

app.MapHeroesApi();

app.Run();
