using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WindowsUpdateManager.ViewModels;

namespace WindowsUpdateManager.Views;

public sealed partial class UpdatesPage : Page
{
    public UpdatesViewModel ViewModel
    {
        get;
    }

    public UpdatesPage()
    {
        ViewModel = App.GetService<UpdatesViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        ViewModel.OnNavigatedTo(e.Parameter);
    }
}
