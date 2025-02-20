using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class SignInPage : ContentPage
{
	public SignInPage(AccountViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}