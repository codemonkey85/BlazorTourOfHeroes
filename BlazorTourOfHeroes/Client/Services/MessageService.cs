namespace BlazorTourOfHeroes.Client.Services;

public record MessageService
{
    public readonly IList<string> Messages = new List<string>();

    public void Add(string message) => Messages.Add(message);

    public void Clear() => Messages.Clear();
}
