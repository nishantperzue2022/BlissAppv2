using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.CountyModule;
using BlissApp.Control;
using BlissApp.DTO.CountyModule;
using BlissApp.DTO.FindHospitalModule;
using BlissApp.Utility;
using System.Collections.ObjectModel;
using BlissApp.Pages.MedicalCentres;
using BlissApp.DTO.MedicalCentres;
using BlissApp.Pages.FindHospital;

namespace BlissApp.ViewModels
{
    [QueryProperty(nameof(Listofmedicalcentre), nameof(Listofmedicalcentre))]
    public partial class CountyViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Listofmedicalcentre listofmedicalcentre;
        public ObservableCollection<CountyDTO> listOfCounties { get; set; } = new();

        private readonly ICountyRepository countyRepository = new CountyRepository();
        public CountyViewModel()
        {

        }

        [RelayCommand]
        public async Task OpenCountiesPage()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await Application.Current.MainPage?.Navigation.PushAsync(new CountiesPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task CountyList()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                var getConties = countyRepository.GetCounties().Item2;

                if (listOfCounties.Count() != 0)
                {
                    listOfCounties.Clear();
                }
                foreach (var item in getConties)
                {
                    listOfCounties.Add(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task GetCountyName(CountyDTO countyDTO)
        {
            try
            {
                if (countyDTO == null)
                {
                    return;
                }
                Preferences.Default.Set("CountyName", countyDTO.Name);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }
        }


        [RelayCommand]
        public async Task BackToFindMedicalCentres()
        {
            await Shell.Current.GoToAsync("..");

        }


        [RelayCommand]
        async Task GoToDetails(Listofmedicalcentre listofmedicalcentre)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                if (listofmedicalcentre == null)
                {
                    return;
                }
                IsBusy = true;

                Preferences.Default.Set("longitude", listofmedicalcentre.Longitude.ToString());

                Preferences.Default.Set("latitude", listofmedicalcentre.Latitude.ToString());

                Preferences.Default.Set("county", listofmedicalcentre.County.ToString());

                Preferences.Default.Set("providerName", listofmedicalcentre.Location.ToString());

                await Task.Delay(1500);

                await Shell.Current.GoToAsync(nameof(MedicalCentreDetailsPage), true, new Dictionary<string, object>
                {
                    {"Listofmedicalcentre", listofmedicalcentre }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", ex.Message));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
