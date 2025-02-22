using BlissApp.ViewModels;

namespace BlissApp.Pages.ContactUs;

public partial class ContactUsPage : ContentPage
{
	public ContactUsPage(ContactusViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;


        Application.Current.UserAppTheme = AppTheme.Light;

    }
}