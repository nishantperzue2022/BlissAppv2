using BlissApp.ViewModels;

namespace BlissApp.Pages.Dashboard;

public partial class UltrasoundPage : ContentPage
{
    public UltrasoundPage(OurServicesViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}