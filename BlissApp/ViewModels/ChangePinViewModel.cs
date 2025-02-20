using BlissApp.BLL.AuthenticationModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.Pages.Account;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace BlissApp.ViewModels
{
    public partial class ChangePinViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _oldPin;

        [ObservableProperty]
        public string _newPin;

        [ObservableProperty]
        public string _confirmPin;

        [ObservableProperty]
        public bool _isOldPinHasError;

        [ObservableProperty]
        public string _oldPinErrorText;

        [ObservableProperty]
        public bool _isNewPinHasError;

        [ObservableProperty]
        public string _newPinErrorText;

        [ObservableProperty]
        public bool _isConfirmPinHasError;

        [ObservableProperty]
        public string _confirmPinErrorText;

        private readonly ILoginRepository loginRepository;
        public ChangePinViewModel(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        [RelayCommand]
        public async Task ChangePin()
        {
            try
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;

                if (accessType != NetworkAccess.Internet)
                {
                   // await Application.Current.MainPage?.ShowPopupAsync(new InternetConnMessage());

                    return;
                }

                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;


                if (string.IsNullOrWhiteSpace(OldPin))
                {
                    OldPinErrorText = "Please enter Old PIN";

                    IsOldPinHasError = true;

                    return;
                }
                else
                {
                    OldPinErrorText = string.Empty;

                    IsOldPinHasError = false;
                }


                if (string.IsNullOrWhiteSpace(NewPin))
                {
                    NewPinErrorText = "Please enter New PIN !";

                    IsNewPinHasError = true;

                    return;
                }
                else
                {
                    NewPinErrorText = string.Empty;

                    IsNewPinHasError = false;
                }


                if (string.IsNullOrWhiteSpace(ConfirmPin))
                {
                    ConfirmPinErrorText = "Please enter Confirm PIN !";

                    IsConfirmPinHasError = true;

                    return;
                }
                else
                {
                    ConfirmPinErrorText = string.Empty;

                    IsConfirmPinHasError = false;
                }


                if (OldPin.Length < 4)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "You have entered invalid Old PIN !. PIN Should be more than 4 digits"));

                    return;
                }

                if (NewPin.Length < 4)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "You have entered invalid New PIN !. PIN Should be more than 4 digits"));

                    return;
                }

                if (ConfirmPin.Length < 4)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "You have entered invalid Confirm PIN !. PIN Should be more than 4 digits"));

                    return;
                }

                else if (NewPin != ConfirmPin)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "New Pin & confirm Pin do not match !"));

                    return;
                }

                if (accessType == NetworkAccess.Internet)
                {
                    var logindetails = Preferences.Get("UserInfo", "0");

                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    ChangePinDTO user = new ChangePinDTO();

                    user.Id = Guid.Parse(userDetails.Id);

                    user.CurrentPin = OldPin;

                    user.NewPin = NewPin;

                    var userdata = await loginRepository.ChangePin(user);

                    if (userdata.Status == true)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", userdata.Message));

                        NewPin = "";

                        ConfirmPin = "";

                        OldPin = "";

                        await Shell.Current.GoToAsync(nameof(SignInPage2));

                        return;

                    }
                    if (userdata.Status == false)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "We are not able to verify your  \n current pin ,please try again"));

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to create an account ,please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }
    }
}
