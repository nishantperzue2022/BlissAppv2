using BlissApp.BLL.OfferModule;
using BlissApp.Control;
using BlissApp.DTO.MemberModule;
using BlissApp.DTO.OfferModule;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BlissApp.ViewModels
{
    public partial class OfferViewModel :BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<OfferDTO> OfferList { get; set; } = new();

        private readonly IOfferRepository  offerRepository;
        public OfferViewModel(IOfferRepository offerRepository)
        {
            this.offerRepository = offerRepository;
        }

        [RelayCommand]
        public async Task GetOffers()
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

                var data = (await offerRepository.GetTestimonials()).Item2;

                var list = data.offers.ToList();

                if (OfferList.Count() != 0)
                {
                    OfferList.Clear();
                }
                foreach (var item in list)
                {
                    OfferList.Add(item);
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
