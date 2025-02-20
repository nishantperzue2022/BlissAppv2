using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class SendNewPinPage : ContentPage
{
    ProfileViewModel _vm;
    public SendNewPinPage()
	{
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new ProfileViewModel();

        BindingContext = _vm;
    }
}