namespace BlazorTourOfHeroes.Client.Services;

public class MessageService
{
    public readonly IList<string> Messages = new List<string>();
    private readonly RefreshService _refreshService;

    public MessageService(RefreshService refreshService) => _refreshService = refreshService;

    public void Add(string message)
    {
        Messages.Add(message);
        _refreshService.CallRequestRefresh();
    }

    public void Clear() => Messages.Clear();
}
