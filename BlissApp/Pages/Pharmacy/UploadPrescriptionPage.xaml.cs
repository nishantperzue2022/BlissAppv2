using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class UploadPrescriptionPage : ContentPage
{
	public UploadPrescriptionPage(PrescriptionViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

        Application.Current.UserAppTheme = AppTheme.Light;
    }
}