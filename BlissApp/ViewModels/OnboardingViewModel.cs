using BlissApp.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BlissApp.ViewModels
{
    public record OnboardingModel(string Image, string Heading, string Description);
    public class OnboardingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<OnboardingModel> OnboardingSteps { get; set; } = new();

        string Consultation_data = OurServices.Consultation().ToString();

        private string Pharmacy_data = OurServices.Pharmacy().ToString();

        private string Dental_data = OurServices.Dental().ToString();

        private string Optical_data = OurServices.Optical().ToString();

        private string Lab_data = OurServices.Laboratory().ToString();

        private string Xray_data = OurServices.Xray().ToString();

        private string Dawa = OurServices.Dawa().ToString();
        public OnboardingViewModel()
        {
            OnboardingSteps?.Clear();
            OnboardingSteps.Add(new OnboardingModel("consultation1.jpg", "Doctor Consultation", Consultation_data));
            OnboardingSteps.Add(new OnboardingModel("Pharmacy1.jpg", "Pharmacy", Pharmacy_data));
            OnboardingSteps.Add(new OnboardingModel("Dental1.jpg", "Dental", Dental_data));
            OnboardingSteps.Add(new OnboardingModel("Optical1.jpg", "Optical Services", Optical_data));
            OnboardingSteps.Add(new OnboardingModel("Laboratory1.jpg", "Laboratory", Lab_data));
            OnboardingSteps.Add(new OnboardingModel("xray1.jpg", "Radiology", Xray_data));
            OnboardingSteps.Add(new OnboardingModel("dawa.jpg", "Dawa Nyumbani", Dawa));
        }

        private bool isLastStep;
        public bool IsLastStep
        {
            get => isLastStep;
            set
            {
                if (isLastStep != value)
                {
                    isLastStep = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLastStep)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotLastStep)));
                }

            }
        }
        public bool IsNotLastStep => !IsLastStep;
     
    }
}
