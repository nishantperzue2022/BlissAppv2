using BlissApp.ViewModels;

namespace BlissApp.Pages.Dashboard;

public partial class XrayPage : ContentPage
{
	public XrayPage(OurServicesViewModel vm)
	{
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}