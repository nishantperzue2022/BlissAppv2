using BlissApp.Pages.Account;
using BlissApp.Pages.Callback;
using BlissApp.Pages.Feedback;
using BlissApp.Pages.ContactUs;
using BlissApp.Pages.Dependants;
using BlissApp.Pages.FamilyMember;
using BlissApp.Pages.HospitalVisit;
using BlissApp.Pages.Telemedicine;
using BlissApp.Pages.FindHospital;
using BlissApp.Pages.MedicalCentres;
using BlissApp.Pages.Appointment;
using BlissApp.Pages.Dashboard;
using BlissApp.Pages.Blogs;
using BlissApp.Pages.Offers;
using BlissApp.Pages.Pharmacy;
using BlissApp.Controls;



namespace BlissApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterMyRoutes();
        }
        public void RegisterMyRoutes()
        {
            try
            {
         
                Routing.RegisterRoute(nameof(VideoPage), typeof(VideoPage));        
                Routing.RegisterRoute(nameof(CartItemsPage), typeof(CartItemsPage));        
                Routing.RegisterRoute(nameof(AddToCartPage), typeof(AddToCartPage));
                Routing.RegisterRoute(nameof(OrderMedicinePage), typeof(OrderMedicinePage));
                Routing.RegisterRoute(nameof(HospitalPage), typeof(HospitalPage));
                Routing.RegisterRoute(nameof(ClinicPage), typeof(ClinicPage));
                Routing.RegisterRoute(nameof(BookAppointmentPage), typeof(BookAppointmentPage));
           
                Routing.RegisterRoute(nameof(SuccessPage), typeof(SuccessPage));
                Routing.RegisterRoute(nameof(OTPVerifyEmailPage), typeof(OTPVerifyEmailPage));
                Routing.RegisterRoute(nameof(SuccessfulPage), typeof(SuccessfulPage));
                Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
                Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
                Routing.RegisterRoute(nameof(DependantDetailPage), typeof(DependantDetailPage));
                Routing.RegisterRoute(nameof(CallbackDetailPage), typeof(CallbackDetailPage));
                Routing.RegisterRoute(nameof(SignInPage2), typeof(SignInPage2));
                Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
                Routing.RegisterRoute(nameof(DependantPage), typeof(DependantPage));
                Routing.RegisterRoute(nameof(HospitalVisitPage), typeof(HospitalVisitPage));
                Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
                Routing.RegisterRoute(nameof(ContactUsPage), typeof(ContactUsPage));
                Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
                Routing.RegisterRoute(nameof(OtpVerificationPageTwo), typeof(OtpVerificationPageTwo));
                Routing.RegisterRoute(nameof(ChangePasswordPageTwo), typeof(ChangePasswordPageTwo));
                Routing.RegisterRoute(nameof(UploadProfilePicPage), typeof(UploadProfilePicPage));
                Routing.RegisterRoute(nameof(UploadEcardPhotoPage), typeof(UploadEcardPhotoPage));

                Routing.RegisterRoute(nameof(RequestCallbackPage), typeof(RequestCallbackPage));
                Routing.RegisterRoute(nameof(ForgotPINPage), typeof(ForgotPINPage));
                Routing.RegisterRoute(nameof(SendNewPinPage), typeof(SendNewPinPage));
                Routing.RegisterRoute(nameof(TelemedicinePage), typeof(TelemedicinePage));
                Routing.RegisterRoute(nameof(FamilyDetailPage), typeof(FamilyDetailPage));
                Routing.RegisterRoute(nameof(EditMemberDetailsPage), typeof(EditMemberDetailsPage));
                Routing.RegisterRoute(nameof(FeedbackHistoryPage), typeof(FeedbackHistoryPage));
                Routing.RegisterRoute(nameof(AddMemberPage), typeof(AddMemberPage));
                Routing.RegisterRoute(nameof(MedicalCentrePage), typeof(MedicalCentrePage));
                Routing.RegisterRoute(nameof(OfferPage), typeof(OfferPage));
                Routing.RegisterRoute(nameof(DeleteAccountPage), typeof(DeleteAccountPage));

                Routing.RegisterRoute(nameof(MedicalCentreDetailsPage), typeof(MedicalCentreDetailsPage));
                Routing.RegisterRoute(nameof(CountiesPage), typeof(CountiesPage));
                Routing.RegisterRoute(nameof(AppointmentPage), typeof(AppointmentPage));
                Routing.RegisterRoute(nameof(DentalPage), typeof(DentalPage));
                Routing.RegisterRoute(nameof(ConsultationPage), typeof(ConsultationPage));
                Routing.RegisterRoute(nameof(DawaPage), typeof(DawaPage));
                Routing.RegisterRoute(nameof(PharmacyPage), typeof(PharmacyPage));
                Routing.RegisterRoute(nameof(LaboratoryPage), typeof(LaboratoryPage));
                Routing.RegisterRoute(nameof(OpticalPage), typeof(OpticalPage));
                Routing.RegisterRoute(nameof(Radiology), typeof(Radiology));
                Routing.RegisterRoute(nameof(UltrasoundPage), typeof(UltrasoundPage));
                Routing.RegisterRoute(nameof(OTPLoginPage), typeof(OTPLoginPage));
                Routing.RegisterRoute(nameof(OtpVerificationForgotPassword), typeof(OtpVerificationForgotPassword));
                Routing.RegisterRoute(nameof(SetPINPage), typeof(SetPINPage));
                Routing.RegisterRoute(nameof(BlogPage), typeof(BlogPage));
                Routing.RegisterRoute(nameof(VlogsPage), typeof(VlogsPage));
                Routing.RegisterRoute(nameof(XrayPage), typeof(XrayPage));
                Routing.RegisterRoute(nameof(AppointmentDetailsPage), typeof(AppointmentDetailsPage));
                Routing.RegisterRoute(nameof(UploadPrescriptionPage), typeof(UploadPrescriptionPage));
                Routing.RegisterRoute(nameof(PrescriptionPage), typeof(PrescriptionPage));



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }
    }
}
