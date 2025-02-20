using BlissApp.BLL.AppointmentModule;
using BlissApp.Control;
using BlissApp.DTO.AppointmentModule;
using BlissApp.DTO.CallBackModule;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace BlissApp.ViewModels
{
    public partial class AppointmentDetailsViewModel :BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<AppointmentDetailsDTO> ListOfAppointments { get; set; } = new();

        private readonly IAppointmentRepository appointmentRepository;
        public AppointmentDetailsViewModel(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
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

                //var validity= await TokenValidator.CheckTokenValidity();

                var data = (await appointmentRepository.GetAppointments()).Item2;

                if(data != null)
                {
                    var list = data.listOfAppointments.ToList();

                    if (ListOfAppointments.Count != 0)
                    {
                        ListOfAppointments.Clear();
                    }
                    foreach (var item in list)
                    {
                        ListOfAppointments.Add(item);
                    }
                }
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

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
