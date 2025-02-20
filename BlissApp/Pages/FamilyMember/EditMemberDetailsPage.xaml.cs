using BlissApp.ViewModels;
namespace BlissApp.Pages.FamilyMember;

public partial class EditMemberDetailsPage : ContentPage
{
	public EditMemberDetailsPage(EditMemberFamilyViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

		BindingContext = vm;
    }

    private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var vm = (EditMemberFamilyViewModel)BindingContext;

        vm.FirstName = txtFirstName.Text;

    }

    private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var vm = (EditMemberFamilyViewModel)BindingContext;

        vm.LastName = txtLastName.Text;
    }

    private void dtDOB_BindingContextChanged(object sender, EventArgs e)
    {
        var vm = (EditMemberFamilyViewModel)BindingContext;

        vm.DateOfBirth= DateTime.Now.ToString();     
    }
}