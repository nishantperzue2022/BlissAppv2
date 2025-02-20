using BlissApp.Control;
using BlissApp.Pages.Appointment;
using BlissApp.Pages.Dashboard;
using BlissApp.Services;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlissApp.ViewModels
{
    public partial class OurServicesViewModel:BaseViewModel
    {
        [ObservableProperty]
        public string _opticalDescription;

        [ObservableProperty]
        public string _consultationDescription;

        [ObservableProperty]
        public string _dentalDescription;

        [ObservableProperty]
        public string _labDescription;

        [ObservableProperty]
        public string _xrayDescription;

        [ObservableProperty]
        public string _ultrasoundDescription;

        [ObservableProperty]
        public string _pharmacyDescription;

        [ObservableProperty]
        public string _image; 

        [ObservableProperty]
        public string _appointmentType; 
        
        [ObservableProperty]
        public string _image1;     

        private string Consultation_data = OurServices.Consultation().ToString();

        private string Pharmacy_data = OurServices.Pharmacy().ToString();

        private string Dental_data = OurServices.Dental().ToString();

        private string Optical_data = OurServices.Optical().ToString();

        private string Lab_data = OurServices.Laboratory().ToString();

        private string Xray_data = OurServices.Xray().ToString();

        private string Dawa_data = OurServices.Dawa().ToString();

        [RelayCommand]
        public void GetOptical()
        {
            Image = "Optical1";
            OpticalDescription = Optical_data;
        }

        [RelayCommand]
        public void GetConsultation()
        {
            Image = "Consultation1";
            ConsultationDescription = Consultation_data;
        }      
        
        [RelayCommand]
        public void GetDawaNyumbani()
        {
            Image = "dawa";
            ConsultationDescription = Dawa_data;
        }

        [RelayCommand]
        public void GetDental()
        {
            Image = "dental1";
            DentalDescription = Dental_data;
        }

        [RelayCommand]
        public void GetLaboratory()
        {
            Image = "Laboratory1";
            LabDescription = Lab_data;
        }

        [RelayCommand]
        public void GetPharmacy()
        {
            Image = "Pharmacy1";
            PharmacyDescription = Pharmacy_data;
        }

        //[RelayCommand]
        //public void GetUltrasound()
        //{
        //    Image = "Ultrasound1";
        //    UltrasoundDescription = Ultrasound_data;
        //}

        [RelayCommand]
        public void GetXray()
        {
            Image = "xray1";
            XrayDescription = Xray_data;
            AppointmentType = "radiology";
        }

        [RelayCommand]
        public async Task RequestAppointment()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;          

                Preferences.Default.Set("AppointmentType", AppointmentType);

                await Shell.Current.GoToAsync(nameof(AppointmentPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        [RelayCommand]
        public async Task Xray()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(XrayPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }     
        
        [RelayCommand]
        public async Task Ultrasound()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(UltrasoundPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
