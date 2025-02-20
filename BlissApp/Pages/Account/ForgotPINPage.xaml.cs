using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class ForgotPINPage : ContentPage
{
    ProfileViewModel _vm;
    public ForgotPINPage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new ProfileViewModel();

        BindingContext = _vm;
    }
}