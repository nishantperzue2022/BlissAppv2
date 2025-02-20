using BlissApp.BLL.AppointmentModule;
using BlissApp.BLL.DepartmentModule;
using BlissApp.BLL.FamilyMemberModule;
using BlissApp.BLL.MedicalCenterModule;
using BlissApp.Control;
using BlissApp.DTO.AppointmentModule;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.DepartmentModule;
using BlissApp.DTO.MedicalCentres;
using BlissApp.DTO.MemberModule;
using BlissApp.Pages.Appointment;
using BlissApp.Pages.FamilyMember;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace BlissApp.ViewModels
{

    public partial class AppointmentViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<FamilyMemberDTO> ListOfFamilyMember { get; set; } = new();
        public ObservableCollection<DepartmentDTO> ListOfDepartment { get; set; } = new();
        public ObservableCollection<Listofmedicalcentre> ListOfMedicalCentre { get; set; } = new();

        private readonly IFamilyMemberRepository familyMemberRepository;

        private readonly IMedicalCenteRepository  medicalCenteRepository;

        private readonly IAppointmentRepository appointmentRepository;

        private readonly IDepartmentRepository departmentRepository;

        [ObservableProperty]
        public int _departmentId;

        [ObservableProperty]
        public int _medicalCentreId;

        [ObservableProperty]
        public string _priorityTime;

        [ObservableProperty]
        public string _relation;

        [ObservableProperty]
        public string _proposedDate;

        [ObservableProperty]
        public string _firstName;

        [ObservableProperty]
        public string _lastName;

        [ObservableProperty]
        bool _depHasError;

        [ObservableProperty]
        public string _depErrorText;

        [ObservableProperty]
        bool _firstNameHasError;

        [ObservableProperty]
        public string _firstNameErrorText;

        [ObservableProperty]
        bool _lastNameHasError;

        [ObservableProperty]
        public string _lastNameErrorText;

        [ObservableProperty]
        public string _minimumDate;

        [ObservableProperty]
        public string _departmentErrorText;

        [ObservableProperty]
        bool _departmentHasError;

        [ObservableProperty]
        public string _priorityTimeErrorText;

        [ObservableProperty]
        bool _priorityTimeHasError;

        [ObservableProperty]
        public string _medicalCentreErrorText;

        [ObservableProperty]
        bool _medicalCentreHasError;

        [ObservableProperty]
        Guid _memberId;

        [ObservableProperty]
        Guid _patientId;

        private TimeSpan _currentTime;
        public TimeSpan CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;

                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        private DepartmentDTO _selectedDepartment;
        public DepartmentDTO SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                SetProperty(ref _selectedDepartment, value);

                if (SelectedDepartment != null)
                {
                    DepartmentId = SelectedDepartment.Id;
                }
            }
        }

        private FamilyMemberDTO _selectedFamilyMember;
        public FamilyMemberDTO SelectedFamilyMember
        {
            get { return _selectedFamilyMember; }
            set
            {
                SetProperty(ref _selectedFamilyMember, value);

                if (SelectedFamilyMember != null)
                {
                    PatientId = SelectedFamilyMember.Id;
                }
            }
        }

        private Listofmedicalcentre _selectedMedicalCentre;
        public Listofmedicalcentre SelectedMedicalCentre
        {
            get { return _selectedMedicalCentre; }
            set
            {
                SetProperty(ref _selectedMedicalCentre, value);

                if (SelectedMedicalCentre != null)
                {
                    MedicalCentreId = SelectedMedicalCentre.Id;
                }
            }
        }

        public AppointmentViewModel(

            IAppointmentRepository appointmentRepository,

            IFamilyMemberRepository familyMemberRepository,

            IMedicalCenteRepository medicalCenteRepository,

            IDepartmentRepository departmentRepository)
        {
            this.appointmentRepository = appointmentRepository;

            this.medicalCenteRepository = medicalCenteRepository;

            this.familyMemberRepository = familyMemberRepository;

            this.departmentRepository = departmentRepository;

            DateTime currentDate = DateTime.Now;

            DateTime newDate = currentDate.AddDays(1);

            MinimumDate = newDate.ToShortDateString();

        }

        [RelayCommand]
        public async Task BookAppointment()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var patientName = string.Empty;

                if (string.IsNullOrWhiteSpace(Relation))
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Please select relation!"));

                    return;
                }

                if (Relation == "Other")
                {
                    if (PatientId == null || PatientId == Guid.Empty)
                    {
                        FirstNameHasError = true;

                        FirstNameErrorText = "Please select a family member";

                        return;
                    }
                    else
                    {
                        FirstNameHasError = false;

                        FirstNameErrorText = "";
                    }
                }


                string AppointmentType = Preferences.Default.Get("AppointmentType", "Unknown");

                if (AppointmentType != "radiology")
                {
                    if (DepartmentId == 0)
                    {
                        DepartmentHasError = true;

                        DepartmentErrorText = "Please  select department";

                        return;
                    }
                    else
                    {
                        DepartmentHasError = false;

                        DepartmentErrorText = "";
                    }
                }
                if (AppointmentType == "radiology")
                {
                    DepartmentId = 2;
                }             

                if (MedicalCentreId == 0)
                {
                    MedicalCentreHasError = true;

                    MedicalCentreErrorText = "Please  select Medical Centre";

                    return;
                }
                else
                {
                    MedicalCentreHasError = false;

                    MedicalCentreErrorText = "";
                }

                if (string.IsNullOrEmpty(CurrentTime.ToString(@"hh\:mm")))
                {
                    PriorityTimeHasError = true;

                    PriorityTimeErrorText = "Please  select priority time";

                    return;
                }
                else
                {
                    PriorityTimeHasError = false;

                    PriorityTimeErrorText = "";
                }

                IsBusy = true;

                AppointmentDTO user = new AppointmentDTO();

                user.MemberId = Guid.Parse(userDetails.Id);

                user.PatientName = patientName;

                user.DepartmentId = DepartmentId;

                user.MedicalCentreId = MedicalCentreId;

                user.PhoneNumber = userDetails.PhoneNumber;

                user.Email = userDetails.Email;

                if (Relation == "Other")
                {
                    user.PatientId = PatientId;
                }
                else
                {
                    user.PatientId = Guid.Parse(userDetails.Id);
                }

                user.AccessToken = userDetails.Access_token;

                user.PriorityTime = CurrentTime.ToString(@"hh\:mm");

                user.ProposedDate = Convert.ToDateTime(ProposedDate);

                //await TokenValidator.CheckTokenValidity();

                var result = await appointmentRepository.Create(user);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Appointment has been requested successfully .Please note the above appointment request will be confirmed via a call in next 2 working hours"));

                    // ClearFields();
                    Application.Current.MainPage = new AppShell();

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

        [RelayCommand]
        public async Task GetDepartments()
        {
            try
            {
                var list = await departmentRepository.GetDepartments();

                if (list.Item1 == true)
                {
                    var departments = list.Item2.ListOfDepartments;

                    var data = departments.OrderBy(x => x.Name).ToList();

                    if (ListOfDepartment.Count != 0)
                    {
                        ListOfDepartment.Clear();
                    }
                    foreach (var item in data)
                    {
                        ListOfDepartment.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }
        }

        [RelayCommand]
        public async Task GetMedicalCentres()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var data = (await medicalCenteRepository.GetMedicalcentre());

                if (data.Item1 == false)
                {
                    //await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

                    return;
                }
                var list = data.Item2.listOfMedicalCentres.OrderBy(x => x.Location);

                if (ListOfMedicalCentre.Count() != 0)
                {
                    ListOfMedicalCentre.Clear();
                }
                foreach (var item in list)
                {
                    ListOfMedicalCentre.Add(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

                return;
            }
            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }
        [RelayCommand]
        public async Task GetFamilyMember()
        {
           
            try
            {            

                //var validity = await TokenValidator.CheckTokenValidity();

                //if (validity)
                //{
                var data = (await familyMemberRepository.GetFamilyMember()).Item2;

                if(data != null)
                {
                    var list = data.ListOfMembers.ToList();

                    if (ListOfFamilyMember.Count() != 0)
                    {
                        ListOfFamilyMember.Clear();
                    }
                    foreach (var item in list)
                    {
                        ListOfFamilyMember.Add(item);
                    }
                }
           

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

        }

        [RelayCommand]
        public async Task AddFamily()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddMemberPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            try
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;

                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;

                Shell.Current.FlyoutIsPresented = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task GoToDetails()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(AppointmentDetailsPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

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
