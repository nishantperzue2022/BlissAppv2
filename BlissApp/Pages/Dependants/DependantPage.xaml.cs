using BlissApp.ViewModels;

namespace BlissApp.Pages.Dependants;

public partial class DependantPage : ContentPage
{
	public DependantPage(DependantsViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;
	}
}