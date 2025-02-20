using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.AuthenticationModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.Pages.Account;
using BlissApp.Utility;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BlissApp.DTO.CountyModule;
using BlissApp.BLL.CountyModule;
using System.Text.RegularExpressions;


namespace BlissApp.ViewModels
{
    public partial class AccountViewModel : BaseViewModel
    {
        public ObservableCollection<CountyDTO> listOfCounties { get; set; } = new();
        public ObservableCollection<NatureOfEngagementDTO> listOfNatureOfEngagement { get; set; } = new();
        public ObservableCollection<GenderDTO> listOfGender { get; set; } = new();

        [ObservableProperty]
        public string _userName;

        [ObservableProperty]
        public string _password;

        [ObservableProperty]
        public string _firstName;

        [ObservableProperty]
        public string _lastName;

        [ObservableProperty]
        public string _email;

        [ObservableProperty]
        public string _natureOfEngagement;

        [ObservableProperty]
        public string _phoneNumber;

        [ObservableProperty]
        public string _schemeId;

        [ObservableProperty]
        public string _confirmPassword;

        [ObservableProperty]
        public string _activeUser;

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
        bool isGps_Enabled;

        [ObservableProperty]
        bool isForgotPinVisible;

        [ObservableProperty]
        bool _firstNameHasError;

        [ObservableProperty]
        public string _firstNameErrorText;


        [ObservableProperty]
        bool _lastNameHasError;

        [ObservableProperty]
        public string _lastNameErrorText;

        [ObservableProperty]
        bool _phoneNoHasError;

        [ObservableProperty]
        public string _phoneNoErrorText;

        [ObservableProperty]
        bool _emailHasError;

        [ObservableProperty]
        public string _emailErrorText;


        [ObservableProperty]
        bool _natureOfEngHasError;

        [ObservableProperty]
        public string _natureOfEngErrorText;

        [ObservableProperty]
        bool _countyHasError;

        [ObservableProperty]
        public string _countyErrorText;


        [ObservableProperty]
        bool _passwordHasError;

        [ObservableProperty]
        public string _errorText;

        [ObservableProperty]
        bool _pinHasError;

        [ObservableProperty]
        public string _pinErrorText;

        [ObservableProperty]
        bool _confirmPinHasError;

        [ObservableProperty]
        public string _confirmPinErrorText;

        [ObservableProperty]
        public string _countyName;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isTermsAndConditionsChecked;

        [ObservableProperty]
        bool isKeepMeSignedInChecked;

        [ObservableProperty]
        bool isTermsAndConditionsVisible;

        [ObservableProperty]
        public string _termsAndConditionsText;

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

        private NatureOfEngagementDTO _selectedNatureOfEngagement;
        public NatureOfEngagementDTO SelectedNatureOfEngagement
        {
            get { return _selectedNatureOfEngagement; }
            set
            {
                SetProperty(ref _selectedNatureOfEngagement, value);

                if (SelectedNatureOfEngagement != null)
                {
                    NatureOfEngagement = SelectedNatureOfEngagement.Name.ToString();
                }
            }
        }

        private CountyDTO _selectedCounty;
        public CountyDTO SelectedCounty
        {
            get { return _selectedCounty; }
            set
            {
                SetProperty(ref _selectedCounty, value);

                if (SelectedCounty != null)
                {
                    CountyName = SelectedCounty.Name.ToString();
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

        private readonly ILoginRepository loginRepository = new LoginRepository();

        private readonly ICountyRepository countyRepository = new CountyRepository();

        public AccountViewModel()
        {
            var logindetails = Preferences.Get("UserInfo", "0");

            IsKeepMeSignedInChecked = false;

            if (logindetails != "0")
            {
                IsForgotPinVisible = true;
            }

            if (logindetails == "0")
            {
                IsForgotPinVisible = false;
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

        [RelayCommand]
        public Task GetNatureOfEngagement()
        {
            var counties = new List<NatureOfEngagementDTO>
            {
            new NatureOfEngagementDTO { Name = "Cash"},

            new NatureOfEngagementDTO { Name = "Insurance" } };

            var data = counties.OrderBy(x => x.Name).ToList();

            if (listOfNatureOfEngagement.Count() != 0)
            {
                listOfNatureOfEngagement.Clear();
            }
            foreach (var item in data)
            {
                listOfNatureOfEngagement.Add(item);
            }
            return Task.CompletedTask;
        }


        [RelayCommand]
        public void GetUserInfo()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var user = userDetails.MemberName;

                    ActiveUser = user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }
        }

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                // IsGps_Enabled = locSettings.isGpsAvailable();
                IsGps_Enabled = true;

                if (IsGps_Enabled == false)
                {
                    await EnableGPSAsync();
                }

                if (IsGps_Enabled == true)
                {
                    if (string.IsNullOrWhiteSpace(PhoneNumber))
                    {
                        PhoneNoHasError = true;

                        PhoneNoErrorText = "Please enter phone no";

                        return;
                    }
                    else
                    {
                        PhoneNoHasError = false;

                        PhoneNoErrorText = "";
                    }

                    IsBusy = true;

                    var Phone_Number = Regex.Replace(PhoneNumber, @"\s+", "");

                    var results = await loginRepository.Login(Phone_Number, Password);

                    if (results != null && results.Status==true)
                    {
                        //if (results.Status == true)
                        //{
                        if (Preferences.ContainsKey(nameof(App.UserInfo)))
                        {
                            Preferences.Remove(nameof(App.UserInfo));
                        }
                        string userdetails = JsonConvert.SerializeObject(results);

                        Preferences.Set(nameof(App.UserInfo), userdetails);

                        Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                        App.UserInfo = results;

                        if (IsKeepMeSignedInChecked == true)
                        {
                            Preferences.Default.Set("KeepMeSigned", true);
                        }
                        else
                        {
                            Preferences.Default.Set("KeepMeSigned", false);
                        }
                        Application.Current.MainPage = new AppShell();

                        return;
                        //}
                        //if (results.Status == false)
                        //{
                        //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "The Username / Password that you have entered is not valid. Please check and try again"));

                        //    return;
                        //}
                    }
                    else
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "The Username / Password that you have entered is not valid. Please check and try again"));

                        return;
                    }
                }

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
        public async Task LoginTwo()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsGps_Enabled = true;
                //IsGps_Enabled = locSettings.isGpsAvailable();

                if (IsGps_Enabled == false)
                {
                    await EnableGPSAsync();
                }

                if (IsGps_Enabled == true)
                {
                    var logindetails = Preferences.Get("UserInfo", "0");

                    var user = JsonConvert.DeserializeObject<Login>(logindetails);

                    PhoneNumber = user.PhoneNumber;

                    if (string.IsNullOrWhiteSpace(PhoneNumber))
                    {
                        PinHasError = true;

                        PinErrorText = "Please enter PIN";

                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        PinHasError = true;

                        PinErrorText = "Please enter PIN";

                        return;
                    }

                    IsBusy = true;

                    var results = await loginRepository.Login(PhoneNumber, Password);

                    if (results != null && results.Status==true)
                    {
                        if (Preferences.ContainsKey(nameof(App.UserInfo)))
                        {
                            Preferences.Remove(nameof(App.UserInfo));
                        }
                        string userdetails = JsonConvert.SerializeObject(results);

                        Preferences.Set(nameof(App.UserInfo), userdetails);

                        Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                        App.UserInfo = results;

                        if (IsKeepMeSignedInChecked == true)
                        {
                            Preferences.Default.Set("KeepMeSigned", true);
                        }
                        else
                        {
                            Preferences.Default.Set("KeepMeSigned", false);
                        }
                        Application.Current.MainPage = new AppShell();

                        return;
                    }

                    if (results.Status == false)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "The Username / Password that you have entered is not valid. Please check and try again"));

                        return;
                    }
                    else
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to login .Please try to login again"));

                        return;
                    }
                }
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

        private async Task EnableGPSAsync()
        {
            try
            {
                await Application.Current.MainPage?.ShowPopupAsync(new InfoMessage());

                //locSettings.OpenSettings();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", ex.Message));

                return;
            }
        }


        [RelayCommand]
        public async Task CreateAccount()
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

                var k = Regex.Replace(PhoneNumber, @"[^\d]", "");

                if (string.IsNullOrWhiteSpace(PhoneNumber))
                {
                    PhoneNoHasError = true;

                    PhoneNoErrorText = "Please enter phone no";

                    return;
                }
                else
                {
                    PhoneNoHasError = false;

                    PhoneNoErrorText = "";
                }


                if (string.IsNullOrWhiteSpace(CountyName))
                {
                    CountyHasError = true;

                    CountyErrorText = "Please select county of residence";

                    return;
                }
                else
                {
                    CountyHasError = false;

                    CountyErrorText = "";
                }


                if (string.IsNullOrWhiteSpace(Password))
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


                if (string.IsNullOrWhiteSpace(ConfirmPassword))
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

                
                if (Password.Length < 4)
                {
                    PinHasError = true;

                    PinErrorText = "You have entered invalid PIN.PIN should be 4 digits";

                    return;
                }    
                
                if (Password.Length > 4)
                {
                    PinHasError = true;

                    PinErrorText = "You have entered invalid PIN.PIN should be 4 digits";

                    return;
                }

                if (IsTermsAndConditionsChecked == false)
                {
                    IsTermsAndConditionsVisible = true;

                    TermsAndConditionsText = "Please accept Terms and conditions";

                    return;
                }
                else
                {
                    IsTermsAndConditionsVisible = false;

                    TermsAndConditionsText = "";

                }

                if (Password != ConfirmPassword)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "PIN & Confirm PIN do not match !"));

                    return;
                }

                IsBusy = true;

                AuthMemberDTO authMemberDTO = new AuthMemberDTO
                {
                    FirstName = Regex.Replace(FirstName, @"\s+", ""),

                    LastName = Regex.Replace(LastName, @"\s+", ""),

                    Password = Regex.Replace(Password, @"\s+", ""),

                    PhoneNumber = Regex.Replace(PhoneNumber, @"\s+", ""),

                    County = CountyName,
                };

                var result = await loginRepository.CreateAccount(authMemberDTO);

                if (result.Item2.Status == true)
                {

                    Preferences.Default.Set("PhoneNumber", Regex.Replace(authMemberDTO.PhoneNumber, @"\s+", ""));

                    Preferences.Default.Set("MemberName", authMemberDTO.FirstName);

                    Preferences.Default.Set("Password", Regex.Replace(authMemberDTO.Password, @"\s+", ""));

                    var id = result.Item2.Id;

                    Preferences.Default.Set("User_Id", id.ToString());

                    AccountViewModel vm = new();

                    //await Shell.Current.GoToAsync(nameof(OtpVerificationPage));

                    await Application.Current.MainPage?.Navigation.PushAsync(new SuccessfulPage());
                }

                if (result.Item2.Status == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", result.Item2.Message));

                    return;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to create an account ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoToOTPPage()
        {
            try
            {
                bool hasPhoneNo = Preferences.Default.ContainsKey("PhoneNumber");

                bool hasMemberName = Preferences.Default.ContainsKey("MemberName");

                if (hasPhoneNo == true && hasMemberName == true)
                {
                    string phoneNumber = Preferences.Default.Get("PhoneNumber", "0");

                    var k = GenerateOTP(phoneNumber);

                    AccountViewModel vm = new();

                    await Application.Current.MainPage?.Navigation.PushAsync(new OtpVerificationPage(vm));
                }
                else
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));

                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));

                return;
            }
        }

        [RelayCommand]
        public async Task VerifyAccountAccount()
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
                AuthMemberDTO user = new();

                bool hasMemberName = Preferences.Default.ContainsKey("User_Id");

                var userId = Preferences.Default.Get("User_Id", "Unknown");

                IsBusy = true;

                if (hasMemberName == true)
                {
                    var result = await loginRepository.VerifyAccountAccount(Guid.Parse(userId));

                    if (result.Item1 == true)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Account has been successfully verified "));

                        string phoneNumber = Preferences.Default.Get("PhoneNumber", "Unknown");

                        string password = Preferences.Default.Get("Password", "Unknown");

                        var results = await loginRepository.Login(phoneNumber, password);

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

                            Application.Current.MainPage = new AppShell();

                            //await Application.Current.MainPage?.Navigation.PushAsync(new AppShell());

                            return;
                        }

                    }
                    if (result.Item1 == false)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your account ,please try again"));

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
        public async Task OTPLogin()
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
                var logindetails = Preferences.Get("UserInfo", "0");

                IsBusy = true;

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var userId = userDetails.Id;

                    string phoneNumber = userDetails.PhoneNumber;

                    string password = userDetails.Password;

                    var result = await loginRepository.VerifyAccountAccount(Guid.Parse(userId));

                    if (result.Item1 == true)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Account has been successfully verified "));

                        var results = await loginRepository.Login(phoneNumber, password);

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

                            Application.Current.MainPage = new AppShell();

                            //await Application.Current.MainPage?.Navigation.PushAsync(new AppShell());

                            return;
                        }

                    }
                    if (result.Item1 == false)
                    {
                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to verify your account ,please try again"));

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
        public async Task ResendOTP()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;
                string phoneNumber = Preferences.Default.Get("PhoneNumber", "0");

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

                OtpDTO otpDTO = new()
                {
                    OTP = onetime_password,

                    PhoneNumber = phoneNumber
                };

                var userdata = await loginRepository.SendOTP(otpDTO);

                Preferences.Default.Set("otp", onetime_password);

                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //return false;

            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task<bool> GenerateOTP(string phoneNumber)
        {
            try
            {

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
        public async Task GoToSignInPage()
        {
            try
            {
                AccountViewModel vm = new();

                await Application.Current.MainPage?.Navigation.PushAsync(new SignInPage(vm));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));

                return;
            }
        }

        [RelayCommand]
        public async Task GoToSignupPage()
        {
            try
            {
                AccountViewModel vm = new();

                await Application.Current.MainPage?.Navigation.PushAsync(new RegisterPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));

                return;
            }
        }

        [RelayCommand]
        public async Task GoToForgotPINPage()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                await Application.Current.MainPage?.Navigation.PushAsync(new ForgotPINPage());

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
        public async Task OTPLoginPage()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                var logindetails = Preferences.Get("UserInfo", "0");

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var otp = await GenerateOTP(userDetails.PhoneNumber);
                }

                await Application.Current.MainPage?.Navigation.PushAsync(new OTPLoginPage());
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

                ForgetPIN authMemberDTO = new ForgetPIN();

                var logindetails = Preferences.Get("UserInfo", "0");

                var user = JsonConvert.DeserializeObject<Login>(logindetails);

                authMemberDTO.PhoneNumber = user.PhoneNumber;

                var sendPassword = await loginRepository.FortgotPin(authMemberDTO);

                await Application.Current.MainPage?.Navigation.PushAsync(new SendNewPinPage());

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
        public async Task VerifyPasswordResetOTP()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                await Application.Current.MainPage?.Navigation.PushAsync(new ChangePasswordPageTwo());

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
        public async Task SendOtp()
        {
            try
            {
                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Otp has been successfully sent to your registered phone number !"));

                AccountViewModel accountViewModel = new();

                await Application.Current.MainPage?.Navigation.PushAsync(new OtpVerificationPageTwo(accountViewModel));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));

                return;
            }
        }

        [RelayCommand]
        public async Task GetDropDownItems()
        {
            await CountyList();

            await GetNatureOfEngagement();

            await GetGender();
        }

        ///New Staff
        [RelayCommand]
        public async Task OTPBack()
        {
            try
            {
                await Shell.Current.GoToAsync("../route");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again !"));

                return;
            }

        }

        [RelayCommand]
        public Task BackToHomePage()
        {
            var route = $"{nameof(WelcomePage)}";
            // we won't wait - await Shell.Current.GoToAsync(route);
            _ = Shell.Current.GoToAsync(route);
            return Task.CompletedTask;
        }


        [RelayCommand]
        public async Task CountyList()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                var getConties = countyRepository.GetCounties().Item2;

                if (listOfCounties.Count() != 0)
                {
                    listOfCounties.Clear();
                }
                foreach (var item in getConties)
                {
                    listOfCounties.Add(item);
                }

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
