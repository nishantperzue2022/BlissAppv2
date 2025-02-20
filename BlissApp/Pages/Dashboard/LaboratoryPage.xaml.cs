using BlissApp.ViewModels;

namespace BlissApp.Pages.Dashboard;

public partial class LaboratoryPage : ContentPage
{
	public LaboratoryPage(OurServicesViewModel vm)
	{
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}