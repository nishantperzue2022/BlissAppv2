using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.Utils;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MemberModule;
using BlissApp.Utility;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using BlissApp.Pages.Account;

namespace BlissApp.ViewModels
{

    [QueryProperty(nameof(MemberDTO), "MemberDTO")]
    public partial class FileUploadViewModel : BaseViewModel
    {

        [ObservableProperty]
        MemberDTO memberDTO;

        [ObservableProperty]
        public string _profileImageSource;

        [ObservableProperty]
        public string _completeEmployeePhotoPath;

        [ObservableProperty]
        ImageSource _myPhoto;

        [ObservableProperty]
        public string _photoPath;
        public FileUploadViewModel()
        {
            GetlogedInUser();
        }

        [RelayCommand]
        public async Task DependantPhotoFromFile()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var file = await MediaPicker.PickPhotoAsync();

                if (file == null)
                {
                    return;
                }

                bool hasKey = Preferences.Default.ContainsKey("DependantId");

                if (hasKey == false)
                {
                    return;
                }

                int autoId = Preferences.Default.Get("DependantId", 0);

                var content = new MultipartFormDataContent
                {
                    { new StreamContent(await file.OpenReadAsync()), "file", file.FileName }
                };

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var accessToken = userDetails.Access_token;

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                string url = MaklAPI.ApiUrl + "api/FileManager/Upload?AutoId=" + autoId;

                httpClient.BaseAddress = new Uri(url);

                var response = await httpClient.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Photo has been uploaded successfully"));

                    var uniqueFileName = autoId + Path.GetExtension(file.FileName);

                    await GetDependantPhoto(uniqueFileName, accessToken);

                    return;
                }

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Photo has not been uploaded ,please try again"));

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", ex.Message));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }


        [RelayCommand]
        public async Task DependantPhotoFromCamera()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var file = await MediaPicker.CapturePhotoAsync();

                if (file == null)
                {
                    return;
                }     

                bool hasKey = Preferences.Default.ContainsKey("DependantId");

                if (hasKey == false)
                {
                    return;
                }

                int autoId = Preferences.Default.Get("DependantId", 0);

                var content = new MultipartFormDataContent
                {
                    { new StreamContent(await file.OpenReadAsync()), "file", file.FileName }
                };

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var accessToken = userDetails.Access_token;

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                string url = MaklAPI.ApiUrl + "api/FileManager/Upload?AutoId=" + autoId;

                httpClient.BaseAddress = new Uri(url);

                var response = await httpClient.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Photo has been uploaded successfully"));

                    var uniqueFileName = autoId + Path.GetExtension(file.FileName);

                    await GetDependantPhoto(uniqueFileName, accessToken);

                    return;
                }

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Photo has not been uploaded ,please try again"));

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", ex.Message));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetDependantPhoto(string ticks, string accessToken)
        {
            try
            {
                string imageName = ticks;

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                string url = MaklAPI.ApiUrl + "api/FileManager/GetFile?FileName=" + imageName;

                httpClient.BaseAddress = new Uri(url);

                var response = await httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var byteArray = Convert.FromBase64String(content);

                    Stream stream = new MemoryStream(byteArray);

                    var imageSource = ImageSource.FromStream(() => stream);

                    MyPhoto = imageSource;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", ex.Message));

                return;
            }
        }

        [RelayCommand]
        public async Task TakeProfilePhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                long FileSize = 0;

                if (photo != null)
                {
                    var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using (var stream = await photo.OpenReadAsync())

                    using (var newStream = File.OpenWrite(newFile))
                    {
                        await stream.CopyToAsync(newStream);

                        FileSize = stream.Length;
                    }

                    Preferences.Set("ProfilePhoto", newFile);

                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Profile photo has been uploaded successfully"));

                    GetlogedInUser();

                    await Shell.Current.GoToAsync(nameof(SignInPage2));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                //await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Error!", "Something went wrong please try again"));
                return;
            }
        }

        [RelayCommand]
        public async Task IsDependantPhotoExist()
        {
            try
            {
                bool hasKey = Preferences.Default.ContainsKey("DependantId");

                if (hasKey == false)
                {
                    return;
                }

                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var accessToken = userDetails.Access_token;

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                int autoId = Preferences.Default.Get("DependantId", 0);

                var uniqueFileName = autoId + ".jpg";

                string url = MaklAPI.ApiUrl + "api/FileManager/GetFile?FileName=" + uniqueFileName;

                httpClient.BaseAddress = new Uri(url);

                var response = await httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var byteArray = Convert.FromBase64String(content);

                    Stream stream = new MemoryStream(byteArray);

                    var imageSource = ImageSource.FromStream(() => stream);

                    MyPhoto = imageSource;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Error!", "Something went wrong please try again"));

                return;
            }
        }


        [RelayCommand]
        public async Task PickProfilePhoto()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                long FileSize = 0;

                if (photo != null)
                {

                    var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using (var stream = await photo.OpenReadAsync())

                    using (var newStream = File.OpenWrite(newFile))
                    {
                        await stream.CopyToAsync(newStream);

                        FileSize = stream.Length;
                    }

                    Preferences.Set("ProfilePhoto", newFile);

                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Profile photo has been uploaded successfully"));

                    GetlogedInUser();

                    await Shell.Current.GoToAsync(nameof(SignInPage2));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                //await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Error!", "Something went wrong please try again"));

                return;
            }
        }


        [RelayCommand]
        public async Task RemovePhoto()
        {
            try
            {
                Preferences.Set("ProfilePhoto", "userprofilenew.png");

                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Profile photo has been removed successfully"));

                //await Shell.Current.GoToAsync("..");

                GetlogedInUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Error!", "Something went wrong please try again"));

                return;
            }
        }

        [RelayCommand]
        public async Task BackToProfileDetails()
        {
            await Shell.Current.GoToAsync("..");
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

    }
}
