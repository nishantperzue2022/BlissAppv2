using BlissApp.BLL.FamilyMemberModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.DepartmentModule;
using BlissApp.DTO.MemberModule;
using BlissApp.Pages.Callback;
using BlissApp.Pages.FamilyMember;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BlissApp.ViewModels
{
    public partial class FamilyMemberViewModel : BaseViewModel
    {
        public ObservableCollection<RelationDTO> listOfRelation { get; set; } = new();
        public ObservableCollection<GenderDTO> listOfGender { get; set; } = new();

        [ObservableProperty]
        public string _memberRelation;

        [ObservableProperty]
        public string _firstName;

        [ObservableProperty]
        public string _lastName;

        [ObservableProperty]
        public string _dateOfBirth;


        [ObservableProperty]
        bool _firstNameHasError;

        [ObservableProperty]
        public string _firstNameErrorText;


        [ObservableProperty]
        bool _lastNameHasError;

        [ObservableProperty]
        public string _lastNameErrorText;

        [ObservableProperty]
        bool _relationHasError;

        [ObservableProperty]
        public string _relationErrorText;

        [ObservableProperty]
        bool _dateOfBHasError;

        [ObservableProperty]
        public string _dateOfBErrorText;

        [ObservableProperty]
        public string _minimumDate;

        private RelationDTO _selectedRelation;
        public RelationDTO SelectedRelation
        {
            get { return _selectedRelation; }
            set
            {
                SetProperty(ref _selectedRelation, value);

                if (SelectedRelation != null)
                {
                    MemberRelation = SelectedRelation.Name;
                }
            }
        }

        [ObservableProperty]
        public string _gender;

        private GenderDTO _selectedGender;
        public GenderDTO SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                SetProperty(ref _selectedGender, value);

                if (SelectedGender != null)
                {
                    Gender = SelectedGender.Name.ToString();
                }
            }
        }
        private readonly IFamilyMemberRepository familyMemberRepository;
        public FamilyMemberViewModel(IFamilyMemberRepository familyMemberRepository)
        {
            this.familyMemberRepository = familyMemberRepository;

            MinimumDate = DateTime.Now.ToShortDateString();

     
        }

        [RelayCommand]
        public Task GetRelation()
        {
            var counties = new List<RelationDTO>
            {
            new RelationDTO { Name = "Son"},
            new RelationDTO { Name = "Spouse"},
            new RelationDTO { Name = "Mother"},
            new RelationDTO { Name = "Father"},
            new RelationDTO { Name = "Daughter" }};
            var data = counties.OrderBy(x => x.Name).ToList();

            if (listOfRelation.Count() != 0)
            {
                listOfRelation.Clear();
            }
            foreach (var item in data)
            {
                listOfRelation.Add(item);
            }
            return Task.CompletedTask;
        }

        [RelayCommand]
        public async Task SubmitData()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    FirstNameHasError = true;

                    FirstNameErrorText = "Please enter first name";

                    return;
                }
                else
                {
                    FirstNameHasError = false;

                    FirstNameErrorText = "";
                }

                if (string.IsNullOrWhiteSpace(LastName))
                {
                    LastNameHasError = true;

                    LastNameErrorText = "Please enter last name";

                    return;
                }
                else
                {
                    LastNameHasError = false;

                    LastNameErrorText = "";
                }

                if (string.IsNullOrWhiteSpace(DateOfBirth))
                {
                    DateOfBHasError = true;

                    DateOfBErrorText = "Please select date of birth";

                    return;
                }
                else
                {
                    DateOfBHasError = false;

                    DateOfBErrorText = "";
                }

                if (string.IsNullOrWhiteSpace(MemberRelation))
                {
                    RelationHasError = true;

                    RelationErrorText = "Please select relation";

                    return;
                }
                else
                {
                    RelationHasError = false;

                    RelationErrorText = "";
                }

                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                FamilyMemberDTO user = new FamilyMemberDTO();

                user.PrincipalMemberId = Guid.Parse(userDetails.Id);

                user.FirstName = FirstName;

                user.LastName = LastName;

                user.Relation = MemberRelation;

                user.Gender = Gender;

                user.DateOfBirth = Convert.ToDateTime(DateOfBirth);

                await TokenValidator.CheckTokenValidity();

                var result = await familyMemberRepository.Create(user);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Family member has been successfully added"));

                    ClearFields();

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
        public void ClearFields()
        {
            FirstName = "";

            LastName = "";
        }

        [RelayCommand]
        public async Task GoToDetails()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FamilyDetailPage));

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
        public Task GetGender()
        {
            var counties = new List<GenderDTO>
            {
            new GenderDTO { Name = "Male"},

            new GenderDTO { Name = "Female"},

            new GenderDTO { Name = "Other" } };

            var data = counties.OrderBy(x => x.Name).ToList();

            if (listOfGender.Count() != 0)
            {
                listOfGender.Clear();
            }
            foreach (var item in data)
            {
                listOfGender.Add(item);
            }
            return Task.CompletedTask;
        }

    }
}
