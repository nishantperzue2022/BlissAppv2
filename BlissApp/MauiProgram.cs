using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using BlissApp.BLL.AuthenticationModule;
using BlissApp.BLL.CallBackModule;
using BlissApp.BLL.ComplaintModule;
using BlissApp.BLL.ContactusModule;
using BlissApp.BLL.CountyModule;
using BlissApp.BLL.MemberModule;
using BlissApp.Pages.Account;
using BlissApp.Pages.Callback;
using BlissApp.Pages.ContactUs;
using BlissApp.Pages.Dependants;
using BlissApp.Pages.HospitalVisit;
using BlissApp.ViewModels;
using BlissApp.Pages.Dashboard;
using BlissApp.BLL.MedicalCenterModule;
using BlissApp.BLL.AppointmentModule;
using BlissApp.Pages.Appointment;
using BlissApp.BLL.FamilyMemberModule;
using BlissApp.Pages.FamilyMember;
using BlissApp.Pages.MedicalCentres;
using BlissApp.Pages.Feedback;
using BlissApp.Pages.FindHospital;
using BlissApp.BLL.TestimonialModule;
using BlissApp.Pages.Settings;
using BlissApp.BLL.OfferModule;
using BlissApp.Pages.Offers;
using BlissApp.BLL.DepartmentModule;
using Syncfusion.Maui.Core.Hosting;
using BlissApp.Pages.Pharmacy;
using BlissApp.Controls;
using BlissApp.BLL.OrderModule;
namespace BlissApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fontawesome-webfont.ttf", "FontAwesome");

                    fonts.AddFont("Poppins-Medium.ttf", "PoppinsMedium");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-Thin.ttf", "PoppinsThin");
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                })

              .UseMauiMaps();




            //Repositories
            builder.Services.AddTransient<IMemberRepository, MemberRepository>();
            builder.Services.AddTransient<IMedicalCenteRepository, MedicalCenteRepository>();
            builder.Services.AddTransient<ICallBackRepository, CallBackRepository>();
            builder.Services.AddTransient<IComplaintRepository, ComplaintRepository>();
            builder.Services.AddTransient<ILoginRepository, LoginRepository>();
            builder.Services.AddTransient<IContactusRepository, ContactusRepository>();
            builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddTransient<ICountyRepository, CountyRepository>();
            builder.Services.AddTransient<IFamilyMemberRepository, FamilyMemberRepository>();
            builder.Services.AddTransient<ITestimonialRepository, TestimonialRepository>();
            builder.Services.AddTransient<IOfferRepository, OfferRepository>();
            builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();


            //Pages
  
            builder.Services.AddTransient<SuccessPage>();
            builder.Services.AddTransient<DependantPage>();
            builder.Services.AddTransient<HospitalVisitPage>();
            builder.Services.AddTransient<FeedbackPage>();
            builder.Services.AddTransient<RequestCallbackPage>();
            builder.Services.AddTransient<CallbackDetailPage>();
            builder.Services.AddTransient<ContactUsPage>();
            builder.Services.AddTransient<OfferPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ChangePasswordPage>();
            builder.Services.AddTransient<AppointmentDetailsPage>();
            builder.Services.AddTransient<PrescriptionPage>();
            builder.Services.AddTransient<CartItemsPage>();

            builder.Services.AddTransient<OrderMedicinePage>();
            builder.Services.AddTransient<UploadPrescriptionPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<OtpVerificationPage>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<AppointmentPage>();
            builder.Services.AddTransient<AddMemberPage>();
            builder.Services.AddTransient<FamilyDetailPage>();
            builder.Services.AddTransient<EditMemberDetailsPage>();
            builder.Services.AddTransient<MedicalCentrePage>();
            builder.Services.AddTransient<FeedbackHistoryPage>();
            builder.Services.AddTransient<MedicalCentreDetailsPage>();
            builder.Services.AddTransient<CountiesPage>();
            builder.Services.AddTransient<ConsultationPage>();
            builder.Services.AddTransient<DawaPage>();
            builder.Services.AddTransient<DentalPage>();
            builder.Services.AddTransient<LaboratoryPage>();
            builder.Services.AddTransient<OpticalPage>();
            builder.Services.AddTransient<PharmacyPage>();
            builder.Services.AddTransient<UltrasoundPage>();
            builder.Services.AddTransient<Radiology>();
            builder.Services.AddTransient<OTPLoginPage>();
            builder.Services.AddTransient<OtpVerificationForgotPassword>();
            builder.Services.AddTransient<SetPINPage>();
            builder.Services.AddTransient<XrayPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<DeleteAccountPage>();
            builder.Services.AddTransient<PharmacyPage>();
            builder.Services.AddTransient<HospitalPage>();
            builder.Services.AddTransient<AddToCartPage>();
            //view models
            builder.Services.AddTransient<AlertViewModel>();
            builder.Services.AddTransient<AccountViewModel>();
            builder.Services.AddTransient<DependantsViewModel>();
            builder.Services.AddTransient<HospitalVisitViewModel>();
            builder.Services.AddTransient<FeedbackViewModel>();
            builder.Services.AddTransient<CallbackViewModel>();
            builder.Services.AddTransient<CallbackDetailsViewModel>();
            builder.Services.AddTransient<ContactusViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<ComplaintDetailViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<AppointmentViewModel>();
            builder.Services.AddTransient<FamilyMemberViewModel>();
            builder.Services.AddTransient<FamilyMemberDetailsViewModel>();
            builder.Services.AddTransient<EditMemberFamilyViewModel>();
            builder.Services.AddTransient<MedicalCentreLocationViewModel>();
            builder.Services.AddTransient<OurServicesViewModel>();
            builder.Services.AddTransient<CountyViewModel>();
            builder.Services.AddTransient<OnboardingViewModel>();
            builder.Services.AddTransient<OfferViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<ChangePinViewModel>();
            builder.Services.AddTransient<AppointmentDetailsViewModel>();
            builder.Services.AddTransient<PrescriptionViewModel>();
            builder.Services.AddTransient<OrderViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
