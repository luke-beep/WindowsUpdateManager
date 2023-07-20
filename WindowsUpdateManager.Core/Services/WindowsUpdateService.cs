using WindowsUpdateManager.Core.Contracts.Services;
using WindowsUpdateManager.Core.Models;

namespace WindowsUpdateManager.Core.Services;
public class WindowsUpdateService : IWindowsUpdateService
{
    private readonly dynamic _updateSession;
    private readonly dynamic _updateSearcher;

    public WindowsUpdateService()
    {
        _updateSession = Activator.CreateInstance(Type.GetTypeFromProgID("Microsoft.Update.Session"));
        if (_updateSession == null)
        {
            return;
        }

        _updateSession.ClientApplicationID = "WindowsUpdateManager";
        _updateSearcher = _updateSession.CreateUpdateSearcher();
    }

    public async Task<IEnumerable<WindowsUpdate>> GetAvailableUpdatesAsync()
    {
        var updates = new List<WindowsUpdate>();

        await Task.Run(() =>
        {
            var searchResult = _updateSearcher.Search("IsInstalled=0");
            for (var i = 0; i < searchResult.Updates.Count; ++i)
            {
                var update = searchResult.Updates.Item(i);
                updates.Add(new WindowsUpdate
                {
                    Title = update.Title,
                    Description = update.Description,
                    isDownloaded = update.IsDownloaded,
                    IsInstalled = update.IsInstalled
                });
            }
        });
        return updates;
    }

    public async Task DownloadUpdateAsync(WindowsUpdate update)
    {
        await Task.Run(() =>
        {
            dynamic updatesToDownload = Activator.CreateInstance(Type.GetTypeFromProgID("Microsoft.Update.UpdateColl"));
            updatesToDownload.Add(update);
            dynamic downloader = _updateSession.CreateUpdateDownloader();
            downloader.Updates = updatesToDownload;
            dynamic downloadResult = downloader.Download();

            if (downloadResult.ResultCode != 2) 
            {
                throw new Exception($"Download failed with result code: {downloadResult.ResultCode}");
            }
        });
    }

    public async Task InstallUpdateAsync(WindowsUpdate update)
    {
        await Task.Run(() =>
        {
            dynamic updatesToInstall = Activator.CreateInstance(Type.GetTypeFromProgID("Microsoft.Update.UpdateColl"));
            updatesToInstall.Add(update);
            dynamic installer = _updateSession.CreateUpdateInstaller();
            installer.Updates = updatesToInstall;
            dynamic installResult = installer.Install();

            if (installResult.ResultCode != 2)
            {
                throw new Exception($"Installation failed with result code: {installResult.ResultCode}");
            }
        });
    }
}