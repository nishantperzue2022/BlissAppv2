using BlissApp.Controls;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.Pages.Account;
using BlissApp.Pages.Pharmacy;
using BlissApp.ViewModels;
using Newtonsoft.Json;

namespace BlissApp
{
    public partial class App : Application
    {
        public static Login UserInfo;
        public App()
        {
            InitializeComponent();

            Current.UserAppTheme = AppTheme.Light;

            Preferences.Clear();

            bool hasKey = Preferences.Default.ContainsKey("FamilyMemberId");

            if (hasKey == true)
            {
                Preferences.Default.Remove("FamilyMemberId");
            }
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH1cd3RRRWJZV0N+X0c=");

            // MainPage = new AppShell();

            var logindetails = Preferences.Get("UserInfo", "0");

            if (logindetails != "0")
            {

                bool KeepMeSignedExist = Preferences.Default.ContainsKey("KeepMeSigned");

                if (KeepMeSignedExist == true)
                {
                    bool KeepMeSignedIstrue = Preferences.Default.Get("KeepMeSigned", false);

                    if (KeepMeSignedIstrue == true)
                    {
                        Current.MainPage = new AppShell();

                        return;
                    }
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var phoneNumber = userDetails.PhoneNumber;

                    if (string.IsNullOrEmpty(phoneNumber))
                    {
                        MainPage = new NavigationPage(new MainPage());

                        return;
                    }
                    else
                    {
                        MainPage = new NavigationPage(new SignInPage2());

                        return;
                    }
                }
            }



            //PrescriptionViewModel vm = new PrescriptionViewModel();

            MainPage = new NavigationPage(new MainPage());

            return;
        }
    }
}
