using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.Control;
using BlissApp.DTO.HomePageModule;
using BlissApp.Pages.Callback;
using BlissApp.Pages.ContactUs;
using BlissApp.Pages.Dependants;
using BlissApp.Pages.HospitalVisit;
using BlissApp.Utility;
using BlissApp.Pages.Feedback;
using BlissApp.Pages.MedicalCentres;
using BlissApp.Pages.Appointment;
using BlissApp.Pages.Dashboard;
using System.Collections.ObjectModel;
using BlissApp.DTO.DashboardModule;
using BlissApp.BLL.TestimonialModule;
using BlissApp.DTO.TestimonialModule;
using BlissApp.Controls;
using BlissApp.Pages.Blogs;
using BlissApp.DTO.MedicalCentres;

namespace BlissApp.ViewModels
{
    [QueryProperty(nameof(HomePageDTO), "HomePageDTO")]
    public partial class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<CarouselDTO> BlogsList { get; set; }
        public ObservableCollection<TestimonialDTO> TestimonialList { get; set; } = new();
        public AsyncRelayCommand RefreshCommand { get; }

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        public string _greet;

        [ObservableProperty]
        public string _lastLogin;

        [ObservableProperty]
        public string _memberScheme;

        [ObservableProperty]
        public string _NoOfDependants;

        [ObservableProperty]
        public string _hosVisits;

        private readonly ITestimonialRepository testimonialRepository = new TestimonialRepository();
        public HomePageViewModel()
        {
            GetBlogs();
        }

        [RelayCommand]
        public async Task GetOffers()
        {
            try
            {
                //if (IsBusy)
                //{
                //    return;
                //}

                //IsBusy = true;


                await Application.Current.MainPage?.ShowPopupAsync(new OfferPopup());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Request has not been sent please try again"));

                return;
            }

            //finally
            //{
            //    IsBusy = false;
            //}
        }


        [RelayCommand]
        public async Task GetTestimonials()
        {
            try
            {

                var data = await testimonialRepository.GetTestimonials();

                var testimonials = data.Item2.testimonial;

                if (testimonials != null)
                {
                    if (TestimonialList.Count() != 0)
                    {
                        TestimonialList.Clear();
                    }
                    foreach (var item in testimonials)
                    {
                        TestimonialList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        [RelayCommand]
        public void GetBlogs()
        {
            //RefreshCommand = new AsyncRelayCommand(Refresh);
         
            BlogsList = new ObservableCollection<CarouselDTO>
            {
                new CarouselDTO() {Category="customers", Title = "Bliss healthcare's happy customers say it all",Description="The nurses who had been assigned to my son’s care were professional...", ImageUrl = "blog1",Type="Blog" },

                new CarouselDTO() {Category="Antenatal", Title = "Why Antenatal Care is Critical for Mother & Child Healthcare",Description="Antenatal Care is an important part of Mother & Child Healthcare...", ImageUrl = "blog2" ,Type="Blog"},

                new CarouselDTO() {Category="Ultrasound", Title = "Why Ultrasound Services are Safe During Pregnancy",Description="Ultrasounds during pregnancy are commonly used to monitor...", ImageUrl = "blog3",Type="Blog" },

                new CarouselDTO() {Category="Planning", Title = "Planning to Space Your Family? Let Bliss Healthcare Assist You",Description="Provision of Family Planning is a very important health intervention...", ImageUrl = "blog4",Type="Blog" },

                new CarouselDTO() {Category="HumanBody", Title = "Here are Some Amazing Facts About the Human Body",Description="Cherophobia is the word for irrational fear of being happy...", ImageUrl = "blog5",Type="Blog" },

                new CarouselDTO() {Category="DrugAbuse",Title = "Drug Abuse: Bliss Healthcare’s Insight Towards this Menacing Epidemic",Description="NN, a young man, 23 years of age, has been consulting the Psychologis...", ImageUrl = "blog6" , Type = "Blog"},

                new CarouselDTO() { Title = "",Description="More about us", ImageUrl = "vlogscover",Type="Vlog" }
            };
        }
        private async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }

        [RelayCommand]
        public async Task Dependants()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(DependantPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task GoToBlogsPage()
        {
            try
            {
                await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/bliss-healthcare", BrowserLaunchMode.SystemPreferred);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task CallBackRequest()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(RequestCallbackPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Complaint()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FeedbackPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task HospitalVisits()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HospitalVisitPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task FindHospital()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(MedicalCentrePage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task ContacUs()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ContactUsPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task BookAppointment()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AppointmentPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Dental()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(DentalPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Consultation()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ConsultationPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }     
        
        [RelayCommand]
        public async Task Dawa()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(DawaPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Pharmacy()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(PharmacyPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Laboratory()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(LaboratoryPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Optical()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(OpticalPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task X_Ray()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(XrayPage), animate: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }     
        
        
        [RelayCommand]
        public async Task NavigateToRadiology()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(Radiology),animate:true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Ultrasound()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(UltrasoundPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }
        [RelayCommand]
        public async Task Whatsap()
        {
            try
            {
                await Browser.OpenAsync("https://api.whatsapp.com/send?phone=+254745330000", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task MentalHealth()
        {
            try
            {
                await Browser.OpenAsync("https://youtu.be/Gvhxp43pDak?si=eL71DVyqP0xqoMjH", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Hypertension()
        {
            try
            {
                await Browser.OpenAsync("https://youtu.be/PpiJb7_1KUY?si=84VeOEFRVNLskpIP", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
       public async Task GoBlogsType(CarouselDTO carouselDTO)
        {
            try
            {
                if (carouselDTO.Type == "Blog")
                {
                    if (carouselDTO.Category == "Antenatal")
                    {
                        await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/why-antenatal-care", BrowserLaunchMode.SystemPreferred);

                        return;
                    }     
                    
                    if (carouselDTO.Category == "Ultrasound")
                    {
                        await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/why-ultrasound", BrowserLaunchMode.SystemPreferred);

                        return;
                    }
                           
                    if (carouselDTO.Category == "Planning")
                    {
                        await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/planning-to-space", BrowserLaunchMode.SystemPreferred);

                        return;
                    }     
                    if (carouselDTO.Category == "HumanBody")
                    {
                        await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/here-are-some-amazing", BrowserLaunchMode.SystemPreferred);

                        return;
                    }   
                    if (carouselDTO.Category == "DrugAbuse")
                    {
                        await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/drug-abuse", BrowserLaunchMode.SystemPreferred);

                        return;
                    }
                        
                    if (carouselDTO.Category == "customers")
                    {
                        await Browser.OpenAsync("https://blissmedicalcentre.com/blog-details/bliss-healthcare", BrowserLaunchMode.SystemPreferred);

                        return;
                    }
                }
                if (carouselDTO.Type == "Vlog")
                {
                    await Shell.Current.GoToAsync(nameof(VlogsPage));

                    return;
                }

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
