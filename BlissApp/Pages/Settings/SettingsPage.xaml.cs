using BlissApp.ViewModels;

namespace BlissApp.Pages.Settings;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }

    //private void btnExit_Clicked(object sender, EventArgs e)
    //{
    //    Application.Current.Quit();
    //}
}