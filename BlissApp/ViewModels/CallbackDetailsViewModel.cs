

using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.CallBackModule;
using BlissApp.Control;
using BlissApp.DTO.CallBackModule;
using BlissApp.Utility;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BlissApp.ViewModels
{
    public partial class CallbackDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<CallBackDTO> listofCallbackHistory { get; set; } = new();

        private readonly ICallBackRepository callBackRepository;      
        public CallbackDetailsViewModel(ICallBackRepository callBackRepository)
        {
            this.callBackRepository = callBackRepository;

            Title = "";
        }

        [RelayCommand]
        public async Task GetHistory()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                //if (connectivity.NetworkAccess != NetworkAccess.Internet)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Please check your internet connectivity and try again"));

                //    return;
                //}

                IsBusy = true;

                await TokenValidator.CheckTokenValidity();

                var data = (await callBackRepository.GetHistory()).Item2;

                var list = data.callbackHistory.ToList();

                if (listofCallbackHistory.Count() != 0)
                {
                    listofCallbackHistory.Clear();
                }
                foreach (var item in list)
                {
                    listofCallbackHistory.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Debug.WriteLine($"Something went wrong:{ex.Message}");

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }


        [RelayCommand]
        public async Task Delete(CallBackDTO callBackDTO)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                //if (connectivity.NetworkAccess != NetworkAccess.Internet)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Please check your internet connectivity and try again"));

                //    return;
                //}

                IsBusy = true;

                await TokenValidator.CheckTokenValidity();

                var result = await callBackRepository.DeleteCallback(callBackDTO);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Success", "Callback request has been successfully deleted"));

                    IsBusy = false;

                    IsRefreshing = true;

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

                Debug.WriteLine($"Something went wrong:{ex.Message}");

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }
    }
}
