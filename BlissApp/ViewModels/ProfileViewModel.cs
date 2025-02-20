using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.AuthenticationModule;
using BlissApp.BLL.Utils;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.Pages.Account;
using BlissApp.Utility;
using Newtonsoft.Json;
using BlissApp.BLL.FamilyMemberModule;
using BlissApp.DTO.MemberModule;
using System.Collections.ObjectModel;
using BlissApp.BLL.CountyModule;
using BlissApp.DTO.CountyModule;
namespace BlissApp.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _photoPath;

        [ObservableProperty]
        public string _memberName;

        [ObservableProperty]
        public string _memberNumber;

        [ObservableProperty]
        public string _phoneNumber;

        [ObservableProperty]
        public string _status;

        [ObservableProperty]
        public string _age;

        [ObservableProperty]
        public string _dateOfBirth;

        [ObservableProperty]
        public string _textColors;

        [ObservableProperty]
        public string _imageUrl;

        [ObservableProperty]
        public string _gender;

        [ObservableProperty]
        public string _firstName;

        [ObservableProperty]
        public string _lastName;

        [ObservableProperty]
        public string _email;

        [ObservableProperty]
        public string _bloodGroup;

        [ObservableProperty]
        public string _otpOne;

        [ObservableProperty]
        public string _otpTwo;

        [ObservableProperty]
        public string _otpThree;

        [ObservableProperty]
        public string _otpFour;

        [ObservableProperty]
        public string _otpFive;

        [ObservableProperty]
        public string _otpSix;

        [ObservableProperty]
        public string _confirmPIN;

        [ObservableProperty]
        public string _newPIN;

        [ObservableProperty]
        bool _pinHasError;

        [ObservableProperty]
        public string _pinErrorText;

        [ObservableProperty]
        bool _confirmPinHasError;

        [ObservableProperty]
        public string _confirmPinErrorText;

        [ObservableProperty]
        bool _emailHasError;

        [ObservableProperty]
        public string _emailErrorText;

        private readonly ILoginRepository loginRepository = new LoginRepository();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        public string _maximumDate;     
        
        [ObservableProperty]
        public bool _emailStatus;

        private BloodGroupDTO _selectedBloodGroup;
        public BloodGroupDTO SelectedBloodGroup
        {
            get { return _selectedBloodGroup; }
            set
            {
                SetProperty(ref _selectedBloodGroup, value);

                if (SelectedBloodGroup != null)
                {
                    BloodGroup = SelectedBloodGroup.Name;
                }
            }
        }
        public ObservableCollection<FamilyMemberDTO> listOfFamilyMember { get; set; } = new();
        public ObservableCollection<BloodGroupDTO> ListOfBloodGroup { get; set; } = new();
        public ObservableCollection<CountyDTO> ListOfCounties { get; set; } = new();

        private readonly IFamilyMemberRepository familyMemberRepository = new FamilyMemberRepository();

        private readonly ICountyRepository countyRepository = new CountyRepository();
        public ProfileViewModel()
        {
            MaximumDate = DateTime.Now.ToShortDateString();

            GetlogedInUser();
        }

        [RelayCommand]
        public void GetlogedInUser()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    Age = userDetails.age;

                    DateOfBirth = userDetails.NewDateOfBith;

                    var status = userDetails.Status;

                    Gender = userDetails.gender;

                    MemberName = userDetails.MemberName;

                    PhoneNumber = userDetails.PhoneNumber;

                    FirstName = userDetails.Firstname;

                    LastName = userDetails.Lastname;

                    Email = userDetails.Email;

                   var IsEmailVerified = userDetails.IsEmailVerified;

                    if (IsEmailVerified == 1)
                    {
                        EmailStatus=false;
                    }
                    else
                    {
                        EmailStatus = true;
                    }

                    if (status == true)
                    {
                        Status = "Active";

                        TextColors = "Green";
                    }

                    if (status == false)
                    {
                        Status = "Inactive";

                        TextColors = "Red";
                    }

                    bool hasKey = Preferences.Default.ContainsKey("ProfilePhoto");

                    if (hasKey == true)
                    {
                        PhotoPath = Preferences.Default.Get("ProfilePhoto", "Unknown");
                    }
                    else
                    {
                        PhotoPath = "userprofilenew.png";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        public async Task UpdateDetails()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                AuthMemberDTO user = new()
                {
                    Id = Guid.Parse(userDetails.Id),

                    DateOfBirth = Convert.ToDateTime(DateOfBirth),

                    Gender = Gender,

                    Email = Email,

                    BloodGroup = BloodGroup,
                };

                await TokenValidator.CheckTokenValidity();

                var result = await loginRepository.UpdateInformation(user);

                if (result.Item1 == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Profile has been successfully updated"));

                    return;
                }
                if (result.Item1 == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your profile,please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your profile,please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task SendEmailVerificationOTP()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                if (string.IsNullOrWhiteSpace(Email))
                {
                    EmailHasError = true;

                    EmailErrorText = "Please enter email";

                    return;
                }
                else
                {
                    EmailHasError = false;

                    EmailErrorText = "";
                }

                var sendOtp = await GenerateOTP();

                if (sendOtp == true)
                {
                    Preferences.Default.Set("tempEmail", Email);

                    await Application.Current.MainPage?.Navigation.PushAsync(new OTPVerifyEmailPage());

                    return;
                }
                else
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your profile,please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your profile,please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }
     

        [RelayCommand]
        public async Task UpdateAccount()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                AuthMemberDTO user = new()
                {
                    Id = Guid.Parse(userDetails.Id),

                    FirstName = FirstName,

                    LastName = LastName,
                };

                await TokenValidator.CheckTokenValidity();

                var result = await loginRepository.UpdateAccount(user);

                if (result.Item1 == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Profile has been successfully updated"));


                    LoginUser();

                    GetlogedInUser();
                    //await Shell.Current.GoToAsync(nameof(SignInPage2));
                }
                if (result.Item1 == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your profile,please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your profile,please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }

        private async void LoginUser()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                IsBusy = true;

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var userId = userDetails.Id;

                    string phoneNumber = userDetails.PhoneNumber;

                    string password = userDetails.Password;

                    var results = await loginRepository.Login(phoneNumber, password);

                    Preferences.Clear();

                    if (results.Status == true)
                    {
                        if (Preferences.ContainsKey(nameof(App.UserInfo)))
                        {
                            Preferences.Remove(nameof(App.UserInfo));
                        }
                        string userdetails = JsonConvert.SerializeObject(results);

                        Preferences.Set(nameof(App.UserInfo), userdetails);

                        Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                        App.UserInfo = results;

                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your account ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task ChangePassword()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }

        }

        [RelayCommand]
        async Task OpenProfilePicPage()
        {
            try
            {
                ProfileViewModel vm = new ProfileViewModel();

                await Shell.Current.GoToAsync(nameof(UploadProfilePicPage));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }

        [RelayCommand]
        async Task ViewProfile()
        {
            try
            {
                Shell.Current.FlyoutIsPresented = false;

                await Shell.Current.GoToAsync(nameof(ProfilePage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }

        }


        [RelayCommand]
        public async Task TakePhoto()
        {
            try
            {

                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo == null)
                {
                    PhotoPath = null;
                    return;
                }

                long FileSize = 0;

                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (var stream = await photo.OpenReadAsync())

                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                    FileSize = stream.Length;

                }

                //if (FileSize > 5000000)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Upload Error!", "File should be less than 5 mbz"));

                //    return;
                //}

                Preferences.Set("ProfilePhoto", newFile);

                //PhotoPath = newFile;
                GetlogedInUser();


                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Your file uploaded successfully"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Upload Error!", "File should be less than 5 mbz"));
                return;
            }
        }

        [RelayCommand]
        public async Task PickPicture()
        {
            try
            {
                //var photo = await MediaPicker.CapturePhotoAsync();
                var photo = await MediaPicker.PickPhotoAsync();
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;

                    return;
                }

                long FileSize = 0;

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (var stream = await photo.OpenReadAsync())

                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                    FileSize = stream.Length;
                }

                //if (FileSize > 5000000)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Upload Error!", "File should be less than 5 mbz"));

                //    return;
                //}

                Preferences.Set("ProfilePhoto", newFile);

                //PhotoPath = newFile;               
                GetlogedInUser();
                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Your photo removed successfully"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Error!", "Something went wrong please try again"));

                return;
            }
        }


        [RelayCommand]
        async Task DeleteAccount()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                if (logindetails == "0")
                {
                    await Shell.Current.GoToAsync(nameof(SignInPage));
                }

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var MemberNo = userDetails.PhoneNumber;

                var httpClient = new HttpClient();

                string url = MaklAPI.ApiUrl + "api/Account/DeleteAccount?MemberNo=" + MemberNo;

                httpClient.BaseAddress = new Uri(url);


                var response = await httpClient.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    Preferences.Clear();

                    await Shell.Current.GoToAsync(nameof(SignInPage));
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", ex.Message));

                return;
            }
        }
        [RelayCommand]
        public async Task RemovePhoto()
        {
            try
            {
                Preferences.Set("ProfilePhoto", "userprofilenew.png");

                PhotoPath = "userprofilenew.png";

                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Your photo has been removed successfully"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Error!", "Something went wrong please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Upload Error!", "Something went wrong,please try again"));

                return;
            }
        }


        [RelayCommand]
        public async Task LoginPage()
        {
            try
            {
                AccountViewModel vm = new AccountViewModel();

                await Application.Current.MainPage?.Navigation.PushAsync(new SignInPage(vm));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Upload Error!", "Something went wrong,please try again"));

                return;
            }
        }


        [RelayCommand]
        public async Task SendNewPin()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                //Thread.Sleep(3000);

                //AuthMemberDTO authMemberDTO = new AuthMemberDTO();

                //var logindetails = Preferences.Get("UserInfo", "0");

                //var user = JsonConvert.DeserializeObject<Login>(logindetails);

                //authMemberDTO.Id = Guid.Parse(user.Id);

                //var sendPassword = await loginRepository.FortgotPin(authMemberDTO);

                await GenerateOTP();

                await Application.Current.MainPage?.Navigation.PushAsync(new OtpVerificationForgotPassword());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));
                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", ex.Message));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task ResendOTP()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await GenerateOTP();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));
                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", ex.Message));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<bool> GenerateOTP()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                if (logindetails == "0")
                {
                    await Shell.Current.GoToAsync(nameof(SignInPage));
                }
                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var phoneNumber = userDetails.PhoneNumber;

                char[] charArr = "0123456789".ToCharArray();

                string strrandom = string.Empty;

                Random objran = new();

                for (int i = 0; i < 6; i++)
                {
                    int pos = objran.Next(1, charArr.Length);

                    if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);

                    else i--;
                }

                var onetime_password = strrandom;

                //var onetime_password = "111111";

                OtpDTO otpDTO = new()
                {
                    OTP = onetime_password,

                    PhoneNumber = phoneNumber
                };

                var userdata = await loginRepository.SendOTP(otpDTO);

                Preferences.Default.Set("otp", onetime_password);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        [RelayCommand]
        public async Task VerifyOTP()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                var getSentOTP = Preferences.Default.Get("otp", "Unknown");

                var otp = OtpOne + OtpTwo + OtpThree + OtpFour + OtpFive + OtpSix;

                if (otp != getSentOTP)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "You have entered invalid otp"));

                    return;
                }

                IsBusy = true;

                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "OTP has been successfully verified"));

                await Application.Current.MainPage?.Navigation.PushAsync(new SetPINPage());

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your account ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task CreateNewPIN()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(NewPIN))
                {
                    PinHasError = true;

                    PinErrorText = "Please enter PIN";

                    return;
                }
                else
                {
                    PinHasError = false;

                    PinErrorText = "";
                }
                if (string.IsNullOrWhiteSpace(ConfirmPIN))
                {
                    ConfirmPinHasError = true;

                    ConfirmPinErrorText = "Please enter Confirm PIN";

                    return;
                }
                else
                {
                    ConfirmPinHasError = false;

                    ConfirmPinErrorText = "";
                }
                if (NewPIN != ConfirmPIN)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "PIN & Confirm PIN do not match !"));

                    return;
                }
                IsBusy = true;

                ForgetPIN authMemberDTO = new ForgetPIN();

                var logindetails = Preferences.Get("UserInfo", "0");

                var user = JsonConvert.DeserializeObject<Login>(logindetails);

                authMemberDTO.PhoneNumber = user.PhoneNumber;

                authMemberDTO.Password = NewPIN;

                var results = await loginRepository.FortgotPin(authMemberDTO);

                if (results == true)
                {
                    Application.Current.MainPage = new AppShell();

                    return;
                }
                if (results == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to update your password,please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your account ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task ResendNewPIN()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                //Thread.Sleep(3000);

                ForgetPIN authMemberDTO = new ForgetPIN();

                var logindetails = Preferences.Get("UserInfo", "0");

                var user = JsonConvert.DeserializeObject<Login>(logindetails);

                authMemberDTO.PhoneNumber = user.PhoneNumber;

                var sendPassword = await loginRepository.FortgotPin(authMemberDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));


                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GetFamilyMember()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                //if (connectivity.NetworkAccess != NetworkAccess.Internet)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Please check your internet connectivity and try again"));

                //    return;
                //}

                IsBusy = true;
                await TokenValidator.CheckTokenValidity();

                var data = (await familyMemberRepository.GetFamilyMember()).Item2;

                var list = data.ListOfMembers.ToList();

                if (listOfFamilyMember.Count() != 0)
                {
                    listOfFamilyMember.Clear();
                }
                foreach (var item in list)
                {
                    listOfFamilyMember.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task Delete(FamilyMemberDTO familyMemberDTO)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                //if (connectivity.NetworkAccess != NetworkAccess.Internet)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Please check your internet connectivity and try again"));

                //    return;
                //}

                IsBusy = true;

                await TokenValidator.CheckTokenValidity();

                var result = await familyMemberRepository.DeleteMember(familyMemberDTO);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Record has been successfully deleted"));

                    IsBusy = false;

                    IsRefreshing = true;

                    return;
                }
                if (result == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request at this time, please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request at this time, please try again"));

                return;
            }

            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }

        [RelayCommand]
        public Task GetBloodGroup()
        {
            var counties = new List<BloodGroupDTO>
            {
             new BloodGroupDTO { Name = "A positive (A+)"},
             new BloodGroupDTO { Name = "A negative (A-)"},
             new BloodGroupDTO { Name = "B positive (B+)"},
             new BloodGroupDTO { Name = "B negative (B-)"},
             new BloodGroupDTO { Name = "AB positive (AB+)"},
             new BloodGroupDTO { Name = "AB negative (AB-)"},
             new BloodGroupDTO { Name = "O positive (O+)"},
             new BloodGroupDTO { Name = "O negative (O-)" } };

            var data = counties.OrderBy(x => x.Name).ToList();

            if (ListOfBloodGroup.Count() != 0)
            {
                ListOfBloodGroup.Clear();
            }
            foreach (var item in data)
            {
                ListOfBloodGroup.Add(item);
            }
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task GetCounties()
        {
            var counties = countyRepository.GetCounties().Item2;

            var data = counties.OrderBy(x => x.Name).ToList();

            if (ListOfCounties.Count() != 0)
            {
                ListOfCounties.Clear();
            }
            foreach (var item in data)
            {
                ListOfCounties.Add(item);
            }
            return Task.CompletedTask;
        }

    }
}
