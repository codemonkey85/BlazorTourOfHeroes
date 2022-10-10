namespace BlazorTourOfHeroes.Client.Components;

public partial class HeroSearchComponent
{
    private IEnumerable<Hero?>? _heroes;

    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(_text))
        {
            _heroes = Array.Empty<Hero>();
        }

        _heroes = await HeroService.SearchHeroes(_text);
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
    }

    private Timer? _timer = null;
    private string _text = string.Empty;

    private string Text
    {
        get => _text;
        set
        {
            if (value == _text)
            {
                return;
            }

            _text = value;
            DisposeTimer();
            _timer = new Timer(300);
            _timer.Elapsed += TimerElapsed_TickAsync;
            _timer.Enabled = true;
            _timer.Start();
        }
    }

    private async void TimerElapsed_TickAsync(object? sender, EventArgs e)
    {
        DisposeTimer();
        await SearchAsync();
    }

    private void DisposeTimer()
    {
        if (_timer is null)
        {
            return;
        }

        _timer.Enabled = false;
        _timer.Elapsed -= TimerElapsed_TickAsync;
        _timer.Dispose();
        _timer = null;
    }
}
