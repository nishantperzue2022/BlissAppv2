using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class SetPINPage : ContentPage
{
    ProfileViewModel _vm;
    public SetPINPage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new ProfileViewModel();

		BindingContext = _vm;
	}
}