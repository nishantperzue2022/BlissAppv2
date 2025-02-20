using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class SignInPage2 : ContentPage
{




    AccountViewModel _vm;
    public SignInPage2()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new AccountViewModel();

        BindingContext = _vm;
    }
}