using BlissApp.ViewModels;

namespace BlissApp.Controls;

public partial class FlyoutHeader : ContentView
{
	ProfileViewModel _vm;
    public FlyoutHeader()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new ProfileViewModel();

		BindingContext = _vm;	
	}
}