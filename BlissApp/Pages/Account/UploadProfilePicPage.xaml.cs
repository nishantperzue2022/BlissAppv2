using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class UploadProfilePicPage : ContentPage
{
    FileUploadViewModel _vm;
    public UploadProfilePicPage()
	{
		InitializeComponent();

        _vm = new FileUploadViewModel();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = _vm;

    }
}