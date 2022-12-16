namespace BlazorTourOfHeroes.Client.Components;

public partial class MessagesComponent : IDisposable
{
    protected override void OnInitialized() => RefreshService.RefreshRequested += StateHasChanged;

    public void Dispose() => RefreshService.RefreshRequested -= StateHasChanged;
}
