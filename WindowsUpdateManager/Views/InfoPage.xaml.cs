using Microsoft.UI.Xaml.Controls;

using WindowsUpdateManager.ViewModels;

namespace WindowsUpdateManager.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.
public sealed partial class InfoPage : Page
{
    public InfoViewModel ViewModel
    {
        get;
    }

    public InfoPage()
    {
        ViewModel = App.GetService<InfoViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
