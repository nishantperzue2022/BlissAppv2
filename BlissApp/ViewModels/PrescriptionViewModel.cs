using Android.Gms.Common.Apis;
using BlissApp.BLL.FamilyMemberModule;
using BlissApp.BLL.MedicalCenterModule;
using BlissApp.BLL.Utils;
using BlissApp.Control;
using BlissApp.Controls;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MedicalCentres;
using BlissApp.Pages.Pharmacy;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace BlissApp.ViewModels
{
    public partial class PrescriptionViewModel : BaseViewModel
    {
        public ObservableCollection<Listofmedicalcentre> ListOfMedicalCentre { get; set; } = new();

        private List<ImageItem> photoPaths = new List<ImageItem>();

        private ObservableCollection<ImageItem> _photoPaths = new ObservableCollection<ImageItem>();
        public ObservableCollection<ImageItem> PhotoPaths
        {
            get => _photoPaths;
            set
            {
                _photoPaths = value;
                OnPropertyChanged();
            }
        }

        private readonly IMedicalCenteRepository medicalCenteRepository = new MedicalCenteRepository();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        public string? _countFile;

    
        public PrescriptionViewModel()
        {

            CountFile = "No prescription has been uploaded";

            PhotoPaths.Clear();
        }

        [RelayCommand]
        public Task ClearList()
        {
            PhotoPaths.Clear();
            return Task.CompletedTask;
        }


        [RelayCommand]
        public async Task PickMultiplePhotos()
        {
            try
            {
                //        var photos = await FilePicker.PickAsync();

                //        // Define the file types to allow only images
                var photos = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images // This allows only image files
                });


                if (photos != null)
                {
                    // Clear the existing photo paths
                    PhotoPaths.Clear();

                    // Process each photo (add path to ObservableCollection)

                    var data = new ImageItem
                    {
                        ImagePath = photos.FullPath,

                        Name = "29895258"
                    };


                    PhotoPaths.Add(data);

                    var k = PhotoPaths.Count();

                    if (k > 0)
                    {
                      

                        CountFile = k + " " + "Prescripton(s) uploaded ";
                    }

                    // You can upload the photos or handle them here
                    // await UploadPhotos(photos);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur
                Console.WriteLine($"Error picking photos: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task TakePhoto()
        {
            try
            {
                var photos = await MediaPicker.CapturePhotoAsync();

                if (photos != null)
                {
                    // Clear the existing photo paths
                    PhotoPaths.Clear();

                    // Process each photo (add path to ObservableCollection)
                    var data = new ImageItem
                    {
                        ImagePath = photos.FullPath,

                        Name = "29895258"
                    };

                    PhotoPaths.Add(data);

                    var k = PhotoPaths.Count();

                    if (k > 0)
                    {

                        CountFile = k + " " + "Prescripton(s) uploaded ";
                    }

                    // You can upload the photos or handle them here
                    // await UploadPhotos(photos);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur
                Console.WriteLine($"Error picking photos: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task UploadPhotos(List<FileResult> photos)
        {
            var k = PhotoPaths.ToList();

            using var client = new HttpClient();
            var content = new MultipartFormDataContent();

            foreach (var photo in k)
            {
                var fileContent = await GetImageBytesAsync(photo.ImagePath);
                var byteContent = new ByteArrayContent(fileContent);
                //var fileContent = new StreamContent(await OpenReadAsync());
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(byteContent, "images", Path.GetFileName(photo.ImagePath));
                content.Add(new StringContent(photo.Name), "names");

                var response = await client.PostAsync("https://e0f0-105-163-157-122.ngrok-free.app/api/Prescription/upload", content);

                if (response.IsSuccessStatusCode)
                {
                    // Console.WriteLine($"Successfully uploaded: {photo.FileName}");
                }
                else
                {
                    // Console.WriteLine($"Error uploading {photo.FileName}: {response.StatusCode}");
                }
            }
        }


        [RelayCommand]
        public async Task UploadImages()
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

                var MemberId = userDetails.Id;

                if(MemberId == null)
                {       

                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Something went wrong ,please try again"));

                    return;

                } 

                var client = new HttpClient();

                using (var content = new MultipartFormDataContent())
                {
                    foreach (var item in PhotoPaths)
                    {
                        // Convert the image to byte[] (either from a URL or local path)
                        var imageBytes = await GetImageBytesAsync(item.ImagePath);

                        var byteContent = new ByteArrayContent(imageBytes);

                        byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                        // Add the image and the name as part of the multipart form data
                        content.Add(byteContent, "images", Path.GetFileName(item.ImagePath));

                        content.Add(new StringContent(MemberId), "MemberId");
                    }

                    // Send the data to the API using POST
                    var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Prescription/SendPrescription", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                        var result = JsonConvert.DeserializeObject<PrescriptionResponse>(JsonResult); // deserialize json to c #                

                        if (result?.Status == true)
                        {
                            PhotoPaths.Clear();                 

                            var s = "Prescription has been successfuly submitted";


                            await Shell.Current.GoToAsync($"{nameof(SuccessPage)}?Text={s}", animate: true);

                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error uploading images and names: " + response.StatusCode);

                        await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Something went wrong ,please try again"));

                        return;

                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Server not available ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Helper method to load image into byte array
        private async Task<byte[]> GetImageBytesAsync(string imagePath)
        {
            // If the image is a URL
            if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
            {
                var httpClient = new HttpClient();
                return await httpClient.GetByteArrayAsync(imagePath);  // Download image as byte[]
            }

            // If the image is a local file path (use file access code if needed)
            return await File.ReadAllBytesAsync(imagePath);  // Read local image file as byte[]
        }

        [RelayCommand]
        public async Task UploadPrescription()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(UploadPrescriptionPage), animate: true);

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
        public async Task HospitalAppointment()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HospitalPage), animate: true);

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
        public async Task ClinicAppointment()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ClinicPage), animate: true);

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
        public async Task BookConsultation()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HospitalPage), animate: true);

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
        public async Task OrderMedicine()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(OrderMedicinePage), animate: true);

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
        public async Task NavigateToBookAppointment(Listofmedicalcentre listofmedicalcentre)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(BookAppointmentPage), animate: true);

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
        public async Task NavigateToCartItems()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(CartItemsPage), animate: true);

                return;
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
            }
        }


        [RelayCommand]
        public async Task GetHospitals()
        {
            if (IsBusy)
            {
                return;
            }
            try
            { 
               IsBusy = true;
        
                var data = (await medicalCenteRepository.GetMedicalcentre()).Item2;


                if (data != null)
                {
                    var list = data.listOfMedicalCentres.Take(10).ToList();

                    if (ListOfMedicalCentre.Count != 0)
                    {
                        ListOfMedicalCentre.Clear();
                    }
                    foreach (var item in list)
                    {
                        ListOfMedicalCentre.Add(item);
                    }
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
    }
}
public class ImageItem
{
    public string Name { get; set; }
    public string ImagePath { get; set; } // This can be a local file path or a URL
}