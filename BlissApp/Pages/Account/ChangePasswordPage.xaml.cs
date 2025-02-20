using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class ChangePasswordPage : ContentPage
{
    public ChangePasswordPage(ChangePinViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
    }
}