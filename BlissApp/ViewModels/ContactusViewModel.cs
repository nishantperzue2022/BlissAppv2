using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.ContactusModule;
using BlissApp.Control;
using BlissApp.Utility;
using BlissApp.Pages.ContactUs;

namespace BlissApp.ViewModels
{
    public partial class ContactusViewModel : BaseViewModel
    {
        
        [ObservableProperty]
        public string _maklLink;

        [ObservableProperty]
        public string _facebookLink;

        [ObservableProperty]
        public string _contactNo;

        [ObservableProperty]
        public string _lindaLink;

        [ObservableProperty]
        public string _infoEmail;

        private readonly IContactusRepository contactusRepository;
        public ContactusViewModel(IContactusRepository contactusRepository)
        {
            this.contactusRepository = contactusRepository;
        }

        [RelayCommand]
        public async Task GetContactDetails()
        {    
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var results = await contactusRepository.GetContactDetails();

                var data = results.Item2.message;

                MaklLink = data.website;

                LindaLink = data.linda;

                InfoEmail = data.email;

                ContactNo = data.phoneNumber;

                FacebookLink = data.facebook;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to login ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        async Task GetDialer()
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)

                    PhoneDialer.Default.Open(ContactNo);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        async Task GetToBlissWebsite()
        {
            try
            {
                await Browser.OpenAsync("https://www.blissmedicalcentre.com/", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }
        [RelayCommand]
        async Task NavigateToVideos()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(VideoPage), animate: true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }
        [RelayCommand]
        async Task GetEmail()
        {
            try
            {
                if (Email.Default.IsComposeSupported)
                {
                    string subject = "";

                    string body = "";

                    string[] recipients = new[] { "info@makl.co.ke" };

                    var message = new EmailMessage
                    {
                        Subject = subject,

                        Body = body,

                        BodyFormat = EmailBodyFormat.PlainText,

                        To = new List<string>(recipients)
                    };

                    await Email.Default.ComposeAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }
        [RelayCommand]
        async Task GetLindaLink()
        {
            try
            {
                //await Browser.OpenAsync(LindaLink, BrowserLaunchMode.SystemPreferred);
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
        public async Task GetFacebookLink()
        {
            try
            {
                await Browser.OpenAsync("https://web.facebook.com/blissmedicalcentre", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task GoToInstagram()
        {
            try
            {
                await Browser.OpenAsync("https://www.instagram.com/blisshealthcare_/", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }



    }
}
