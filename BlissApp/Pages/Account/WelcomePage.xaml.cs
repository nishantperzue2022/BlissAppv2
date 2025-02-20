using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;
    }

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        AccountViewModel vm = new AccountViewModel();

         await Application.Current.MainPage?.Navigation.PushAsync(new RegisterPage());
      
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        AccountViewModel vm = new AccountViewModel();

        await Application.Current.MainPage?.Navigation.PushAsync(new SignInPage(vm));
    }

}