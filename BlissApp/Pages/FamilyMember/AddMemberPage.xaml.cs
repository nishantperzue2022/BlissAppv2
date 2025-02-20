using BlissApp.ViewModels;

namespace BlissApp.Pages.FamilyMember;

public partial class AddMemberPage : ContentPage
{
	public AddMemberPage(FamilyMemberViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext =vm;	
	}

    private void Entry_Focused(object sender, FocusEventArgs e)
    {

    }
}