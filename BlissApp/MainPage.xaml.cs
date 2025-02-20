using BlissApp.Pages.Account;
using BlissApp.ViewModels;

namespace BlissApp
{

    public partial class MainPage : ContentPage
    {
        OnboardingViewModel _vm;
        public MainPage()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = AppTheme.Light;

            _vm = new OnboardingViewModel();

            BindingContext = _vm;

        }

        private void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            _vm.IsLastStep = e.CurrentPosition == (_vm.OnboardingSteps.Count - 1);
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            AccountViewModel vm = new AccountViewModel();

            await Application.Current.MainPage?.Navigation.PushAsync(new RegisterPage());

        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            AccountViewModel vm = new AccountViewModel();

            await Application.Current.MainPage?.Navigation.PushAsync(new RegisterPage());

        }



    }
}
