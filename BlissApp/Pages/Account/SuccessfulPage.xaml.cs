using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;
public partial class SuccessfulPage : ContentPage
{

    AccountViewModel _vm;
    public SuccessfulPage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new AccountViewModel();

        BindingContext = _vm;
    }

}