namespace BlazorTourOfHeroes.Client.Components;

public partial class HeroSearchComponent
{
    private IEnumerable<Hero?>? heroes;

    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            heroes = Array.Empty<Hero>();
        }
        heroes = await HeroService.SearchHeroes(text);
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
    }

    private Timer? timer = null;

    private string text = string.Empty;
    private string Text
    {
        get => text;
        set
        {
            if (value == text)
            {
                return;
            }
            text = value;
            DisposeTimer();
            timer = new Timer(300);
            timer.Elapsed += TimerElapsed_TickAsync;
            timer.Enabled = true;
            timer.Start();
        }
    }

    private async void TimerElapsed_TickAsync(object? sender, EventArgs e)
    {
        DisposeTimer();
        await SearchAsync();
    }

    private void DisposeTimer()
    {
        if (timer is null)
        {
            return;
        }
        timer.Enabled = false;
        timer.Elapsed -= TimerElapsed_TickAsync;
        timer.Dispose();
        timer = null;
    }
}
