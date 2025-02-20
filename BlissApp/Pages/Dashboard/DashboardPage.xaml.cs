using BlissApp.ViewModels;

namespace BlissApp.Pages.Dashboard;

public partial class DashboardPage : ContentPage
{
    private HomePageViewModel _vm;
    public DashboardPage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm= new HomePageViewModel();

        BindingContext = _vm;

        Application.Current.UserAppTheme = AppTheme.Light;
       
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            Shell.Current.FlyoutIsPresented = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
           
        }
    }

    protected override void OnAppearing()
    {
        try
        {
            var list = new HealthTipsViewModel().GetBannerList();

            cvBanner.ItemsSource = list;

            var timer = Application.Current.Dispatcher.CreateTimer();

            timer.Interval = TimeSpan.FromSeconds(3);

            timer.Tick += (s, e) => cvBanner.Position = (cvBanner.Position + 1) % list.Count;

            timer.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
      
    }
}