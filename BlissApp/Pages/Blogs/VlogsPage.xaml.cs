using BlissApp.ViewModels;

namespace BlissApp.Pages.Blogs;

public partial class VlogsPage : ContentPage
{
	HomePageViewModel _vm;
    public VlogsPage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new HomePageViewModel();

        BindingContext = _vm;


    }
}