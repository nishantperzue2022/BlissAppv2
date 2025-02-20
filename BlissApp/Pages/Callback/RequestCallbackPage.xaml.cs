using BlissApp.ViewModels;

namespace BlissApp.Pages.Callback;

public partial class RequestCallbackPage : ContentPage
{
	public RequestCallbackPage(CallbackViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;

    }
}