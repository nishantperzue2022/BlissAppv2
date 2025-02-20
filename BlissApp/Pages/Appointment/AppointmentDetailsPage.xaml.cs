using BlissApp.ViewModels;

namespace BlissApp.Pages.Appointment;

public partial class AppointmentDetailsPage : ContentPage
{
	public AppointmentDetailsPage(AppointmentDetailsViewModel vm)
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

#if IOS
    UINavigationController vc = (UINavigationController)Platform.GetCurrentUIViewController();//using UIKit, find the UINavigationController  
    vc.NavigationBar.Hidden = true;
    CoreGraphics.CGRect frame = vc.NavigationBar.Frame;
    vc.NavigationBar.Frame = new CoreGraphics.CGRect(frame.X,frame.Y - frame.Height,frame.Width,0);
#endif
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
#if IOS
    UINavigationController vc = (UINavigationController)Platform.GetCurrentUIViewController();//using UIKit, find the UINavigationController  
    vc.NavigationBar.Frame = new CoreGraphics.CGRect(barX,barY,barWidth,barHeight);
    vc.NavigationBar.Hidden = false;
#endif
    }
}