using BlissApp.ViewModels;

namespace BlissApp.Pages.Dependants;

public partial class DependantDetailPage : ContentPage
{
    DependantDetailsViewModel _vm;
    public DependantDetailPage()
	{
		InitializeComponent();

        _vm = new DependantDetailsViewModel();

        BindingContext = _vm;


    }
}