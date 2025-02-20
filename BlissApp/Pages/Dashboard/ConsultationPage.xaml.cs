using BlissApp.ViewModels;
using System;

namespace BlissApp.Pages.Dashboard;

public partial class ConsultationPage : ContentPage
{
    public ConsultationPage(OurServicesViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;    

    }
}