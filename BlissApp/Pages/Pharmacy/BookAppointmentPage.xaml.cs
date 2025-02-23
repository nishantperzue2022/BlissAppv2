using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class BookAppointmentPage : ContentPage
{
	public BookAppointmentPage(ConsultationViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;

        Application.Current.UserAppTheme = AppTheme.Light;
    }
}