using BlissApp.BLL.FamilyMemberModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MemberModule;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace BlissApp.ViewModels
{

    [QueryProperty(nameof(FamilyMemberDTO), nameof(FamilyMemberDTO))]
    public partial class EditMemberFamilyViewModel : BaseViewModel
    {

        [ObservableProperty]
        FamilyMemberDTO familyMemberDTO;

        [ObservableProperty]
        public string _memberRelation;

        [ObservableProperty]
        public Guid _id;

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

        private readonly IFamilyMemberRepository familyMemberRepository;
        public EditMemberFamilyViewModel(IFamilyMemberRepository familyMemberRepository)
        {
            this.familyMemberRepository = familyMemberRepository;

            //GetRelation();
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

                //if (string.IsNullOrWhiteSpace(MemberRelation))
                //{
                //    RelationHasError = true;

                //    RelationErrorText = "Please select relation";

                //    return;
                //}
                //else
                //{
                //    RelationHasError = false;

                //    RelationErrorText = "";
                //}


                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                FamilyMemberDTO user = new FamilyMemberDTO();

                var MemberId = Preferences.Default.Get("FamilyMemberId", "Unknown");

                user.Id = Guid.Parse(MemberId);

                user.FirstName = FirstName;

                user.LastName = LastName;

                user.Relation = MemberRelation;

                user.DateOfBirth = Convert.ToDateTime(DateOfBirth);

                await TokenValidator.CheckTokenValidity();

                var result = await familyMemberRepository.Edit(user);

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

            MemberRelation = "";
        }
    }
}
