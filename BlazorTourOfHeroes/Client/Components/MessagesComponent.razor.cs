namespace BlazorTourOfHeroes.Client.Components;

public partial class MessagesComponent
{
    protected override void OnInitialized() => RefreshService.RefreshRequested += StateHasChanged;
}
