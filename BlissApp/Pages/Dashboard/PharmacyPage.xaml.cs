using BlissApp.ViewModels;
namespace BlissApp.Pages.Dashboard;

public partial class PharmacyPage : ContentPage
{
	public PharmacyPage(OurServicesViewModel vm)
	{
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}