using BlissApp.ViewModels;

namespace BlissApp.Pages.Dependants;

public partial class UploadEcardPhotoPage : ContentPage
{

	FileUploadViewModel _vm;
    public UploadEcardPhotoPage()
	{
		InitializeComponent();

		_vm = new FileUploadViewModel();

		BindingContext = _vm;	

	}
}