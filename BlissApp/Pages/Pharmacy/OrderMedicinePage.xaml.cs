using BlissApp.ViewModels;

namespace BlissApp.Pages.Pharmacy;

public partial class OrderMedicinePage : ContentPage
{
    public OrderMedicinePage(OrderViewModel vm) 
    {
        InitializeComponent();

        BindingContext = vm;

        Application.Current.UserAppTheme = AppTheme.Light;
    }

    //private async void OnSelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    //{
    //    try
    //    {
    //        if (autocomplete != null && autocomplete.SelectedValue != null)
    //        {

    //            await Shell.Current.GoToAsync(nameof(AddToCartPage), animate: true);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);

    //        return;
    //    }



    //}
}