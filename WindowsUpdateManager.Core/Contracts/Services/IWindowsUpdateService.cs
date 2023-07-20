using WindowsUpdateManager.Core.Models;

namespace WindowsUpdateManager.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface IWindowsUpdateService
{
    Task<IEnumerable<WindowsUpdate>> GetAvailableUpdatesAsync();
    Task DownloadUpdateAsync(WindowsUpdate update);
    Task InstallUpdateAsync(WindowsUpdate update);
}