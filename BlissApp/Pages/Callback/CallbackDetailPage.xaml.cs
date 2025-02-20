using BlissApp.ViewModels;

namespace BlissApp.Pages.Callback;

public partial class CallbackDetailPage : ContentPage
{
	public CallbackDetailPage(CallbackDetailsViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

		BindingContext = vm;
    }
}