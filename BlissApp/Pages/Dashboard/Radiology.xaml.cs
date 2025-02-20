using BlissApp.ViewModels;
namespace BlissApp.Pages.Dashboard;
public partial class Radiology : ContentPage
{
	public Radiology(OurServicesViewModel vm)
	{
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}