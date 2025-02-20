using BlissApp.ViewModels;

namespace BlissApp.Pages.Telemedicine;

public partial class TelemedicinePage : ContentPage
{
    HomePageViewModel _vm;
    public TelemedicinePage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new HomePageViewModel();

        BindingContext = _vm;   
    }
}