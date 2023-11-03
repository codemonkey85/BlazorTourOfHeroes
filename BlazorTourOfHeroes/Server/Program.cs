var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllersWithViews();
services.AddRazorPages();

services.AddDbContextFactory<AppDbContext>(options => options.UseInMemoryDatabase(nameof(AppDbContext)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
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

var apiGroup = app.MapGroup("api");

apiGroup.MapHeroesApi();

app.Run();
