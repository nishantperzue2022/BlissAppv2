using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class AddToCartPage : ContentPage
{
	public AddToCartPage(CartViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}