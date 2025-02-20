using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.ComplaintModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.Utility;
using Newtonsoft.Json;
using BlissApp.DTO.DepartmentModule;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using BlissApp.DTO.MedicalCentres;
using BlissApp.BLL.MedicalCenterModule;
using BlissApp.Pages.Feedback;
using BlissApp.DTO.FeedbackModule;
using BlissApp.DTO.MemberModule;
using BlissApp.BLL.FamilyMemberModule;
using BlissApp.Pages.FamilyMember;
using BlissApp.BLL.DepartmentModule;

namespace BlissApp.ViewModels
{
    public partial class FeedbackViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentDTO> ListOfDepartment { get; set; } = new();
        public ObservableCollection<FeedbackTypeDTO> ListOfFeedbackType { get; set; } = new();
        public ObservableCollection<Listofmedicalcentre> ListOfMedicalCentre { get; set; } = new();
        public ObservableCollection<FamilyMemberDTO> ListOfFamilyMember { get; set; } = new();

        private readonly IMedicalCenteRepository medicalCenteRepository;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool _dateHasError;

        [ObservableProperty]
        public string _dateErrorText;

        [ObservableProperty]
        bool _medCenterHasError;

        [ObservableProperty]
        public string _departmentErrorText;

        [ObservableProperty]
        bool _departmentHasError;

        [ObservableProperty]
        public string _medCentreErrorText;

        [ObservableProperty]
        bool _descriptionHasError;

        [ObservableProperty]
        public string _descriptionErrorText;

        [ObservableProperty]
        bool _feedbackTypeHasError;

        [ObservableProperty]
        public string _feedbackTypeErrorText;

        [ObservableProperty]
        public string _department;

        [ObservableProperty]
        public string _description;

        [ObservableProperty]
        public int _medicalCentreId;

        [ObservableProperty]
        public string _selectedDateOfVisit;

        [ObservableProperty]
        public string _minimumDate;

        [ObservableProperty]
        Guid _patientId;

        [ObservableProperty]
        string _feedbackType;

        [ObservableProperty]
        string _buttonText;

        [ObservableProperty]
        public int _departmentId;

        [ObservableProperty]
        public string _relation;


        [ObservableProperty]
        bool _firstNameHasError;

        [ObservableProperty]
        public string _firstNameErrorText;
        public List<FeedbackDTO> Items { get; set; }

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

        private FeedbackTypeDTO _selectedFeedbackType;
        public FeedbackTypeDTO SelectedFeedbackType
        {
            get { return _selectedFeedbackType; }
            set
            {
                SetProperty(ref _selectedFeedbackType, value);

                if (SelectedFeedbackType != null)
                {
                    FeedbackType = SelectedFeedbackType.Name;

                    ButtonText = "Submit" + " " + FeedbackType;
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

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IComplaintRepository complaintRepository;

        private readonly IFamilyMemberRepository familyMemberRepository;

        private readonly IDepartmentRepository departmentRepository;
        public FeedbackViewModel(IComplaintRepository complaintRepository,
            IMedicalCenteRepository medicalCenteRepository,
            IDepartmentRepository departmentRepository,
            IFamilyMemberRepository familyMemberRepository)
        {
            this.complaintRepository = complaintRepository;

            this.medicalCenteRepository = medicalCenteRepository;

            this.familyMemberRepository = familyMemberRepository;

            this.departmentRepository = departmentRepository;

            MinimumDate = DateTime.Now.ToShortDateString();

            if (string.IsNullOrWhiteSpace(ButtonText))
            {
                ButtonText = "Submit";
            }

            SelectedDateOfVisit = DateTime.Now.ToShortDateString();
        }

        [RelayCommand]
        public async Task SubmitFeedBack()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                if (MedicalCentreId == 0)
                {
                    MedCenterHasError = true;

                    MedCentreErrorText = "Please  select medical centre";

                    return;
                }
                else
                {
                    MedCenterHasError = false;

                    MedCentreErrorText = "";
                }

                if (string.IsNullOrEmpty(SelectedDateOfVisit))
                {
                    DateHasError = true;

                    DateErrorText = "Please select date";

                    return;
                }
                else
                {
                    DateHasError = false;

                    DateErrorText = "";
                }

                if (string.IsNullOrEmpty(Description))
                {
                    DescriptionHasError = true;

                    DescriptionErrorText = "Please enter description";

                    return;
                }
                else
                {
                    DescriptionHasError = false;

                    DescriptionErrorText = "";
                }

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


                if (string.IsNullOrEmpty(FeedbackType))
                {
                    FeedbackTypeHasError = true;

                    FeedbackTypeErrorText = "Please  select department";

                    return;
                }
                else
                {
                    FeedbackTypeHasError = false;

                    FeedbackTypeErrorText = "";
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


                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                FeedbackDTO user = new FeedbackDTO();

                if (Relation == "Other")
                {
                    user.PatientId = PatientId;
                }
                else
                {
                    user.PatientId = Guid.Parse(userDetails.Id);
                }

                user.MemberId = Guid.Parse(userDetails.Id);

                user.MedicalCentreId = MedicalCentreId;

                user.DepartmentId = DepartmentId;

                user.Description = Description;

                user.PhoneNumber = userDetails.PhoneNumber;

                user.MemberName = userDetails.MemberName;

                user.DateOfVisit = Convert.ToDateTime(SelectedDateOfVisit);

                await TokenValidator.CheckTokenValidity();

                var result = await complaintRepository.Create(user);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Your feedback  has been successfully submitted .Our support team will get back to you "));

                    ClearFields();

                    return;
                }
                if (result == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Feedback has not been submited please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Request has not been sbmited please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }
        public void ClearFields()
        {
            Description = "";
        }


        [RelayCommand]
        public async Task GetDepartments()
        {
            var list = await departmentRepository.GetDepartments();

            var departments = list.Item2.ListOfDepartments;

            var data = departments.OrderBy(x => x.Name).ToList();

            if (data.Count > 0)
            {
                if (ListOfDepartment.Count() != 0)
                {
                    ListOfDepartment.Clear();
                }
                foreach (var item in data)
                {
                    ListOfDepartment.Add(item);
                }
            }

        }

        [RelayCommand]
        public Task GetFeedbackType()
        {
            try
            {
                var counties = new List<FeedbackTypeDTO>
            {
            new() { Name = "Complain"},

            new() { Name = "Compliment"},

            new() { Name = "Enquiry"},

            new() { Name = "Request" }
            };

                var data = counties.OrderBy(x => x.Name).ToList();

                if (ListOfFeedbackType.Count() != 0)
                {
                    ListOfFeedbackType.Clear();
                }
                foreach (var item in data)
                {
                    ListOfFeedbackType.Add(item);
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        [RelayCommand]
        async Task GoToDetails()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FeedbackHistoryPage));

                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }

        }


        [RelayCommand]
        public async Task GetMedicalCentres()
        {

            try
            {

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

                Debug.WriteLine($"Something went wrong:{ex.Message}");

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

                return;
            }

        }

        [RelayCommand]
        async Task AddFamily()
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
        public async Task GetFamilyMember()
        {
            //if (IsBusy)
            //{
            //    return;
            //}
            try
            {
                //IsBusy = true;

                // await TokenValidator.CheckTokenValidity();

                var data = (await familyMemberRepository.GetFamilyMember()).Item2;

                if(data != null)
                {
                    var list = data.ListOfMembers.ToList();

                    if (list.Count != 0)
                    {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

            //finally
            //{
            //    IsBusy = false;

            //    IsRefreshing = false;
            //}

        }


    }
}
