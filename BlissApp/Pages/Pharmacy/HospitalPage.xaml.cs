using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class HospitalPage : ContentPage
{
	public HospitalPage(PrescriptionViewModel vm)
	{
		InitializeComponent();

		BindingContext=vm;	

        Application.Current.UserAppTheme = AppTheme.Light;
    }
}