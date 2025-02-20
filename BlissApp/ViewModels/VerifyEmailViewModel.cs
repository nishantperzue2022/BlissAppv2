using BlissApp.BLL.AuthenticationModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace BlissApp.ViewModels
{
    public partial  class VerifyEmailViewModel : BaseViewModel
    {


        [ObservableProperty]
        public string _otpOne;

        [ObservableProperty]
        public string _otpTwo;

        [ObservableProperty]
        public string _otpThree;

        [ObservableProperty]
        public string _otpFour;

        [ObservableProperty]
        public string _otpFive;

        [ObservableProperty]
        public string _otpSix;

        private readonly ILoginRepository loginRepository = new LoginRepository();

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task VerifyEmail()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                //if (string.IsNullOrWhiteSpace(Email))
                //{
                //    EmailHasError = true;

                //    EmailErrorText = "Please enter email";

                //    return;
                //}
                //else
                //{
                //    EmailHasError = false;

                //    EmailErrorText = "";
                //}

                var getSentOTP = Preferences.Default.Get("otp", "Unknown");

                var otp = OtpOne + OtpTwo + OtpThree + OtpFour + OtpFive + OtpSix;

                if (otp != getSentOTP)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "You have entered invalid otp"));

                    return;
                }

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var userId = Guid.Parse(userDetails.Id);

                var email = Preferences.Default.Get("tempEmail", "Unknown");

                VerifyEmailDTO verifyEmailDTO = new()
                {
                    Id = userId,

                    Email = email,
                };

                await TokenValidator.CheckTokenValidity();

                var result = await loginRepository.VerifyEmail(verifyEmailDTO);

                if (result.status == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Email has been successfully verified"));

                    LoginUser();

                   //GetlogedInUser();

                    await Shell.Current.GoToAsync("..");

                }
                if (result.status == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your email,please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your email,please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }

        private async void LoginUser()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                IsBusy = true;

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var userId = userDetails.Id;

                    string phoneNumber = userDetails.PhoneNumber;

                    string password = userDetails.Password;

                    var results = await loginRepository.Login(phoneNumber, password);

                   // Preferences.Clear();

                    if (results.Status == true)
                    {
                        if (Preferences.ContainsKey(nameof(App.UserInfo)))
                        {
                            Preferences.Remove(nameof(App.UserInfo));
                        }
                        string userdetails = JsonConvert.SerializeObject(results);

                        Preferences.Set(nameof(App.UserInfo), userdetails);

                        Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                        App.UserInfo = results;

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your account ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
