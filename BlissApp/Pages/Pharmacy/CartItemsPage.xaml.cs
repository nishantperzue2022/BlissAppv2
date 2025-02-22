using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class CartItemsPage : ContentPage
{
	public CartItemsPage(OrderViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        Application.Current.UserAppTheme = AppTheme.Light;
    }
}