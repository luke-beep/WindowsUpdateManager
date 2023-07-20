namespace WindowsUpdateManager.Core.Models;
public class WindowsUpdate
{
    public string Title
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
    public bool isDownloaded
    {
        get; set;
    }
    public bool IsInstalled
    {
        get; set;
    }
}