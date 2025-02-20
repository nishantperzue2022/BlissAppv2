using BlissApp.ViewModels;

namespace BlissApp.Pages.FindHospital;

public partial class CountiesPage : ContentPage
{
    CountyViewModel _vm;
    public CountiesPage()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm=new CountyViewModel();

        BindingContext = _vm;
    }

}