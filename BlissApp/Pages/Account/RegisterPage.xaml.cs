using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class RegisterPage : ContentPage
{
    AccountViewModel _vm;
    public RegisterPage()
    {
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new AccountViewModel();

        BindingContext = _vm;
    }
}