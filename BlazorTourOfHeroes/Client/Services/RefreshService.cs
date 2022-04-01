namespace BlazorTourOfHeroes.Client.Services
{
    public class RefreshService
	{
        public event Action? RefreshRequested;

        public void CallRequestRefresh() => RefreshRequested?.Invoke();

    }
}
