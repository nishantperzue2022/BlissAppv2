using BlissApp.ViewModels;

namespace BlissApp.Pages.Dashboard;

public partial class DentalPage : ContentPage
{
    public DentalPage(OurServicesViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}