using BlissApp.ViewModels;

namespace BlissApp.Pages.Appointment;

public partial class AppointmentPage : ContentPage
{
    public AppointmentPage(AppointmentViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;

        IsOtherMembers.IsVisible = false;
    }

    protected override void OnAppearing()
    {
        IsOtherMembers.IsVisible = false;

        self.IsChecked= true;

        string AppointmentType = Preferences.Default.Get("AppointmentType", "Unknown");

        if(AppointmentType == "radiology")
        {
            Department.IsVisible = false;
        } 
        if(AppointmentType != "radiology")
        {
            Department.IsVisible = true;
        }

    }


    private void CheckBox_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {

        if (self.IsChecked == true)
        {
            IsOtherMembers.IsVisible = false;

            txtRelation.Text = self.Text;
        }
        else
        {
            IsOtherMembers.IsVisible = true;

            txtRelation.Text = other.Text;

        }
    }
}