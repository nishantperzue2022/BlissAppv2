using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.CallBackModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.CallBackModule;
using BlissApp.Pages.Callback;
using BlissApp.Utility;
using Newtonsoft.Json;

namespace BlissApp.ViewModels
{
    public partial class CallbackViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _phoneNumber;

        [ObservableProperty]
        public string _description;


        [ObservableProperty]
        bool _isPhoneNoHasError;

        [ObservableProperty]
        public string _phoneNoErrorText;


        [ObservableProperty]
        bool _isDescriptionHasError;

        [ObservableProperty]
        public string _descriptionErrorText;


        private readonly ICallBackRepository callBackRepository;
        public CallbackViewModel(ICallBackRepository callBackRepository)
        {
            this.callBackRepository = callBackRepository;

            var logindetails = Preferences.Get("UserInfo", "0");

            var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

            PhoneNumber = userDetails.PhoneNumber;
        }
        [RelayCommand]
        public async Task SubmitCallback()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(PhoneNumber))
                {
                    IsPhoneNoHasError = true;

                    PhoneNoErrorText = "Please enter phone number";

                    return;
                }
                else
                {
                    IsPhoneNoHasError = false;

                    PhoneNoErrorText = "";
                }


                if (string.IsNullOrWhiteSpace(Description))
                {
                    IsDescriptionHasError = true;

                    DescriptionErrorText = "Please enter description";

                    return;
                }
                else
                {
                    IsDescriptionHasError = false;

                    DescriptionErrorText = "";
                }

                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                CallBackDTO user = new CallBackDTO();

                user.MemberId = Guid.Parse(userDetails.Id);

                user.MemberName = userDetails.MemberName;

                user.CallBackReasons = Description;

                user.PhoneNumber = PhoneNumber;

                user.AccessToken = userDetails.Access_token;

                await TokenValidator.CheckTokenValidity();

                var result = await callBackRepository.Create(user);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Callback request has been successfully sent"));

                    ClearFields();

                    return;
                }
                if (result == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Request has not been sent please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Request has not been sent please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }
        public void ClearFields()
        {
            Description = "";
        }

        [RelayCommand]
        public async Task GoToDetails()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CallbackDetailPage));

                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }

        }
    }
}
