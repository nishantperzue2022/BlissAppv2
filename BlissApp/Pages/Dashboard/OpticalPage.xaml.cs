using BlissApp.ViewModels;

namespace BlissApp.Pages.Dashboard;

public partial class OpticalPage : ContentPage
{
    public OpticalPage(OurServicesViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext=vm;  
    }
}