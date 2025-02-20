using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class PrescriptionPage : ContentPage
{
	public PrescriptionPage(PrescriptionViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

        Application.Current.UserAppTheme = AppTheme.Light;
    }
}