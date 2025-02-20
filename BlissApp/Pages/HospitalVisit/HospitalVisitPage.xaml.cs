using BlissApp.ViewModels;

namespace BlissApp.Pages.HospitalVisit;

public partial class HospitalVisitPage : ContentPage
{
	public HospitalVisitPage(HospitalVisitViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;	
	}
}