using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.Control;
using BlissApp.DTO.HospitalVisitModule;
using BlissApp.Utility;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BlissApp.DTO.MedicalCentres;
using BlissApp.BLL.MedicalCenterModule;

namespace BlissApp.ViewModels
{
    public partial class HospitalVisitViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<Listofmedicalcentre> listOfMedicalCentre { get; set; } = new();

        private readonly IMedicalCenteRepository hospitalVisitRepository;
        public HospitalVisitViewModel(IMedicalCenteRepository hospitalVisitRepository)
        {
            this.hospitalVisitRepository = hospitalVisitRepository;
        }

        [RelayCommand]
        public async Task GetHospitalVisit()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {  

                IsBusy = true;

                await TokenValidator.CheckTokenValidity();

                var data = (await hospitalVisitRepository.GetMedicalcentre());

                if (data.Item1 == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

                    return;
                }
                var list = data.Item2.listOfMedicalCentres;

                if (listOfMedicalCentre.Count() != 0)
                {
                    listOfMedicalCentre.Clear();
                }
                foreach (var item in list)
                {
                    listOfMedicalCentre.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Debug.WriteLine($"Something went wrong:{ex.Message}");

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

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
