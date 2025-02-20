using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class AddToCartPage : ContentPage
{
	public AddToCartPage(OrderViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}