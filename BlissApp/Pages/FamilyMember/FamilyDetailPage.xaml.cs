using BlissApp.ViewModels;

namespace BlissApp.Pages.FamilyMember;

public partial class FamilyDetailPage : ContentPage
{	
    public FamilyDetailPage(FamilyMemberDetailsViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;    

        BindingContext = vm;
    }
}