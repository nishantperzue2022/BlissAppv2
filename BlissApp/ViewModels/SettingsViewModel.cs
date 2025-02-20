using BlissApp.BLL.AuthenticationModule;
using BlissApp.Control;
using BlissApp.Pages.Account;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlissApp.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _deleteReason;

        [ObservableProperty]
        public string _description;

        private readonly ILoginRepository loginRepository;
        public SettingsViewModel(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        [RelayCommand]
        public async Task ManageBiometrics()
        {
            try
            {
                //await Shell.Current.GoToAsync(nameof(FingerPrintPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task ChangePassword()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ChangePasswordPage));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
        }

        [RelayCommand]
        async Task ResetApplication()
        {
            try
            {
                Preferences.Clear();

                Application.Current.MainPage = new NavigationPage(new MainPage());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task ExitApplication()
        {
            try
            {
                Application.Current.Quit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task GoToDeleteAccountPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(DeleteAccountPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
        }


        [RelayCommand]
        public async Task DeleteAccount()
        {
            try
            {
                var result = await loginRepository.DeleteAccount();

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Warning", "Account has been deleted"));

                    Preferences.Clear();

                    Application.Current.MainPage = new NavigationPage(new MainPage());

                }
                else
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
        }

    }
}
