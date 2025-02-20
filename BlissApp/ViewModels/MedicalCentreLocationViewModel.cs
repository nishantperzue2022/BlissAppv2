using BlissApp.BLL.MedicalCenterModule;
using BlissApp.Control;
using BlissApp.DTO.MedicalCentres;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BlissApp.ViewModels
{
    public partial class MedicalCentreLocationViewModel:BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<Listofmedicalcentre> listOfMedicalCentres { get; set; } = new();

        private readonly IMedicalCenteRepository medicalCentreLocationRepository;
        public MedicalCentreLocationViewModel(IMedicalCenteRepository medicalCentreLocationRepository)
        {
            this.medicalCentreLocationRepository = medicalCentreLocationRepository;
        }


        [RelayCommand]
        public async Task GetMedicalcentre()
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

                var data = (await medicalCentreLocationRepository.GetMedicalcentre()).Item2;

                var list = data.listOfMedicalCentres.OrderByDescending(x=>x.Location).ToList();

                if (listOfMedicalCentres.Count() != 0)
                {
                    listOfMedicalCentres.Clear();
                }
                foreach (var item in list)
                {
                    listOfMedicalCentres.Add(item);
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
