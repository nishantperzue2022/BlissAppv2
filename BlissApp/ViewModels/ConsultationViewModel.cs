using BlissApp.BLL.ConsultationModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.ConsultationModule;
using BlissApp.DTO.MedicalCentres;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace BlissApp.ViewModels
{
    [QueryProperty(nameof(Listofmedicalcentre), nameof(Listofmedicalcentre))]
    public partial class ConsultationViewModel : BaseViewModel
    {
        private readonly IConsultationRepository consultationRepository;

        [ObservableProperty]
        Listofmedicalcentre listofmedicalcentre;

        [ObservableProperty]
        string _minimumDate;     
        
        
        [ObservableProperty]
        string _errorColor;

        [ObservableProperty]
        string _dateSelectionErrorText; 
        
        [ObservableProperty]
        bool _dateSelectionHasrror;

        private Color _textColor;

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ConsultationViewModel(IConsultationRepository consultationRepository)
        {
            this.consultationRepository = consultationRepository;


            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("yyyy/MM/dd");

            MinimumDate = formattedDate;


        }

        [RelayCommand]
        public async Task BookConsultation()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }


                if (SelectedDate == DateTime.MinValue)
                {
                    DateSelectionHasrror = true;

                    DateSelectionErrorText = "Please select date";

                    ErrorColor = "Red";

                    return;
                }
                else
                {
                    DateSelectionHasrror = false;

                    DateSelectionErrorText = "";

                    ErrorColor = "#009e4e";
                }
                

                if (string.IsNullOrEmpty(listofmedicalcentre.Location))
                {
                    DateSelectionHasrror = true;

                    DateSelectionErrorText = "Please select hospital";

                    ErrorColor = "Red";

                    return;
                }
                else
                {
                    DateSelectionHasrror = false;

                    DateSelectionErrorText = "";

                    ErrorColor = "#009e4e";
                }


                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                IsBusy = true;

                var order = new ConsultationDTO()
                {
                    MemberId = Guid.Parse(userDetails.Id),

                    HospitalId = listofmedicalcentre.Id,

                    ConsultationsDate = SelectedDate
                };

                var token = userDetails.Access_token;

                var result = await consultationRepository.Create(order, token);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Your consultation has been successfully booked"));

                    //await Shell.Current.GoToAsync(nameof(CartItemsPage), animate: true);            

                    return;
                }
                if (result == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request, please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request, please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }











    }
}
