using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    private void male_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        //if (male.IsChecked == true)
        //{
        //    txtGender.Text = male.Text;
        //}
        //if (female.IsChecked == true)
        //{
        //    txtGender.Text = female.Text;
        //}
        //if (other.IsChecked == true)
        //{
        //    txtGender.Text = other.Text;
        //}
    }


}