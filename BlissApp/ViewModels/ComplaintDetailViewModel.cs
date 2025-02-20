using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.ComplaintModule;
using BlissApp.Control;
using BlissApp.Utility;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BlissApp.DTO.FeedbackModule;

namespace BlissApp.ViewModels
{
    public partial class ComplaintDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<FeedbackDetailDTO> Feedbacks { get; set; } = new();

        private readonly IComplaintRepository complaintRepository; 
        public ComplaintDetailViewModel(IComplaintRepository complaintRepository)
        {
            this.complaintRepository = complaintRepository;

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

                //await TokenValidator.CheckTokenValidity();

                var data = (await complaintRepository.GetHistory()).Item2;

                if(data != null)
                {
                    var list = data.ListOfFeedbacks.ToList();

                    if (Feedbacks.Count != 0)
                    {
                        Feedbacks.Clear();
                    }
                    foreach (var item in list)
                    {
                        Feedbacks.Add(item);
                    }
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
        public async Task Delete(FeedbackDTO complaintDTO)
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

                var result = await complaintRepository.DeleteComplaint(complaintDTO);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Callback request has been successfully deleted"));

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
