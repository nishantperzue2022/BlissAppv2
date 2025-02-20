using BlissApp.ViewModels;
using System;

namespace BlissApp.Pages.Dashboard;

public partial class DawaPage : ContentPage
{
    public DawaPage(OurServicesViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;    

    }
}