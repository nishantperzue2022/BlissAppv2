using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class DeleteAccountPage : ContentPage
{
    public DeleteAccountPage(SettingsViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    private void Privacy_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (Privacy.IsChecked == true)
        {
            Cover.IsChecked = false;
            Details.IsChecked = false;
            Other.IsChecked = false;
            txtDeleteReason.Text = Privacy.Text;
        }
    }

    private void Cover_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (Cover.IsChecked == true)
        {
            Privacy.IsChecked = false;
            Details.IsChecked = false;
            Other.IsChecked = false;
            txtDeleteReason.Text = Cover.Text;
        }
    }

    private void Details_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (Details.IsChecked == true)
        {
            Privacy.IsChecked = false;
            Cover.IsChecked = false;
            Other.IsChecked = false;
            txtDeleteReason.Text = Details.Text;
        }
    }

    private void Other_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (Other.IsChecked == true)
        {
            Privacy.IsChecked = false;
            Cover.IsChecked = false;
            Details.IsChecked = false;
            txtDeleteReason.Text = Other.Text;
        }
    }
}