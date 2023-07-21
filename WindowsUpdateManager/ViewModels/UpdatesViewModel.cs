using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using WindowsUpdateManager.Contracts.ViewModels;
using WindowsUpdateManager.Core.Contracts.Services;
using WindowsUpdateManager.Core.Models;

namespace WindowsUpdateManager.ViewModels;

public partial class UpdatesViewModel : ObservableRecipient, INavigationAware
{
    private readonly IWindowsUpdateService _windowsUpdateService;

    [ObservableProperty]
    private WindowsUpdate? selected;

    public ObservableCollection<WindowsUpdate> AvailableUpdates { get;
        set; } = new ObservableCollection<WindowsUpdate>();

    public UpdatesViewModel(IWindowsUpdateService windowsUpdateService)
    {
        _windowsUpdateService = windowsUpdateService;
    }

    public void OnNavigatedTo(object parameter)
    {
    }

    public async Task LoadUpdatesAsync()
    {
        AvailableUpdates.Clear();

        var updates = await _windowsUpdateService.GetAvailableUpdatesAsync();

        foreach (var update in updates)
        {
            AvailableUpdates.Add(update);
        }
    }

    public async void DownloadAndInstallUpdate(WindowsUpdate update)
    {
        await _windowsUpdateService.DownloadUpdateAsync(update);
        await _windowsUpdateService.InstallUpdateAsync(update);
    }


    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        if (AvailableUpdates.Any())
        {
            Selected ??= AvailableUpdates.First();
        }
    }
}
