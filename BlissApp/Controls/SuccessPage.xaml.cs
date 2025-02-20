using BlissApp.ViewModels;
using Java.Time;

namespace BlissApp.Controls;
public partial class SuccessPage : ContentPage
{
    public SuccessPage(AlertViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Application.Current.UserAppTheme = AppTheme.Light;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await img.ScaleTo(1.5).ConfigureAwait(false);
        await msg.ScaleTo(1).ConfigureAwait(false);

        await img.ScaleTo(0.5);
        await img.ScaleTo(1.5);
        await img.ScaleTo(0.5);
        await img.ScaleTo(1.5);
        await img.ScaleTo(0.5);
        await img.ScaleTo(1.5);
        await img.ScaleTo(1);

        await homebtn.FadeTo(1, length: 500).ConfigureAwait(false);
        await homebtn.ScaleTo(1).ConfigureAwait(false);
        await homebtn.ScaleTo(1);
    }

    public void Homebtn_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);

        //await Shell.Current.GoToAsync(nameof(HomePage), animate: true);
        Application.Current.MainPage = new AppShell();

    }
}

