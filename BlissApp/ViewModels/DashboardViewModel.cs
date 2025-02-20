using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BlissApp.ViewModels
{
    public class DashboardViewModel
    {
        public string URL { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
    }

    public class DashboardModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<OnboardingModel> OnboardingSteps { get; set; } = new();
        public List<DashboardViewModel> GetBannerList()
        {
            List<DashboardViewModel> list = new List<DashboardViewModel>
            {
                   new DashboardViewModel { URL = "laboratory.jpg", Heading= "Consultation",Description=
                "Our board-certified doctors are dedicated to understanding each patient's medical history, personality,and preferences, " +
                "ensuring comprehensive healthcare. We provide a wide array of services, from routine check-ups to chronic disease management. " },

              new DashboardViewModel { URL = "pharmacy.jpg", Heading= "Pharmacy",Description=
                "Bliss Healthcare phramacy offers personalized support, high-quality medications from reputable " +
                "sources, convenient services, and affordable prices. Our pharmacy services include prescription" +
                "medications for chronic conditions, over-the-counter medications, and specialty medications for" +
                "complex medical needs." },

            new DashboardViewModel
            {
                URL = "dental.jpg",  Heading = "Dental",Description =
               "We offer a wide range of services including regular check-ups and cleanings, restorative treatments like  " +
               "llings and crowns, cosmetic procedures such as teeth whitening and veneers, and specialized care in" +
               " orthodontics," +
               " endodontics, periodontics, and oral surgery." },

              new DashboardViewModel { URL = "laboratory.jpg", Heading= "Optical",Description=
                "Bliss Healthcare provides comprehensive" +
                " and high-quality optical " +
                " services with a team of skilled optometrists and opticians using state-of-the-art technology." +
                " We offer a variety of services including " +
                " comprehensive eye exams, contact lens  prescription glasses and much more.." },


              new DashboardViewModel { URL = "laboratory.jpg", Heading= "Laboratory",Description=
                "With 65 laboratories," +
                " we're Kenya's largest network, staffed by " +
                "licensed medical technologists and phlebotomists for prompt and precise test results." +
                " Our cutting-edge equipment and daily control testing" +
                " ensure accuracy."},

              new DashboardViewModel { URL = "x_ray.jpg", Heading= "XRAY",Description=
               "The clinic offers a comprehensive array of " +
                "digital X-ray diagnostics, " +
                "including chest, abdominal, skeletal, and dental X-rays to effectively diagnose and monitor conditions" +
                " such as lung infections," +
                " broken bones," +
                "and gastrointestinal issues. With state-of-the-art equipment and a focus on patient convenience."},

              new DashboardViewModel { URL = "laboratory.jpg", Heading= "Ultrasound",Description=
                 "Bliss Healthcare in " +
                "Kenya excels in providing top-tier ultrasound services, " +
                "utilizing the latest technology to produce high-quality images for accurate diagnosis. " +
                "We offer a comprehensive range of ultrasound services, such as obstetric, abdominal, pelvic," +
                " breast, and thyroid ultrasounds."}
            };
            return list;
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
