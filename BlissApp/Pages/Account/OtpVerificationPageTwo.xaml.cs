using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class OtpVerificationPageTwo : ContentPage
{
	public OtpVerificationPageTwo(AccountViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}