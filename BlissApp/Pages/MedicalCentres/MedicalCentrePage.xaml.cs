using BlissApp.BLL.MedicalCenterModule;
using BlissApp.Control;
using BlissApp.DTO.FindHospitalModule;
using BlissApp.DTO.MedicalCentres;
using BlissApp.ViewModels;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
namespace BlissApp.Pages.MedicalCentres;
public partial class MedicalCentrePage : ContentPage
{
    public ObservableCollection<Listofmedicalcentre> ListOfMedicalCentre { get; set; } = new();
    public ObservableCollection<MapLocation> listofLocations { get; set; } = new();

    private readonly IMedicalCenteRepository medicalCenteRepository = new MedicalCenteRepository();

    CountyViewModel _vm;
    public MedicalCentrePage(MedicalCentreLocationViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new CountyViewModel();

        BindingContext = _vm;
    }
    protected override async void OnAppearing()
    {
        try
        {
            bool hasKey = Preferences.Default.ContainsKey("CountyName");

            string countyName = Preferences.Default.Get("CountyName", "0");

            if (hasKey == true && countyName != "0")
            {
                await GetByCountyName();
            }
            else
            {
                await GetAllCentres();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

            return;
        }
    }

    private async void btnNearMe_Clicked(object sender, EventArgs e)
    {
        try
        {
            await GetHospitalsNearMe();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request at this time , please try again later"));

            return;
        }
    }

    public int getDistance(string origin, string destination)
    {
        Thread.Sleep(1000);
        int distance = 0;
        string key = "AIzaSyCsQ_A63oZz9dkeyMJEgsVkpq0iYvDKX10";

        string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&key=" + key;
        url = url.Replace(" ", "+");
        string content = fileGetContents(url);
        JObject o = JObject.Parse(content);
        try
        {
            distance = (int)o.SelectToken("routes[0].legs[0].distance.value");

            return distance;
        }
        catch
        {
            return distance;
        }
    }

    protected string fileGetContents(string fileName)
    {
        string sContents = string.Empty;

        string me = string.Empty;
        try
        {
            if (fileName.ToLower().IndexOf("https:") > -1)
            {
                System.Net.WebClient wc = new System.Net.WebClient();

                byte[] response = wc.DownloadData(fileName);

                sContents = System.Text.Encoding.ASCII.GetString(response);
            }
            else
            {
                var sr = new System.IO.StreamReader(fileName);
                sContents = sr.ReadToEnd();
                sr.Close();
            }
        }
        catch { sContents = "unable to connect to server "; }

        return sContents;
    }


    public async Task GetHospitalsNearMe()
    {
        try
        {
            double latitude = 0;

            double longitude = 0;

            double distance = 0;

            PermissionStatus result = await CheckAndRequestLocationPermission();

            if (result == PermissionStatus.Granted)
            {
                Location myLocation = await Geolocation.GetLastKnownLocationAsync();

                if (myLocation == null)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new InfoMessage());

                    return;
                }

                if (myLocation != null)
                {
                    latitude = myLocation.Latitude;

                    longitude = myLocation.Longitude;

                    List<Listofmedicalcentre> list = new List<Listofmedicalcentre>();

                    var locatons = (await medicalCenteRepository.GetMedicalcentre()).Item2;

                    var providerlist = locatons.listOfMedicalCentres.Where(x => x.Latitude != null && x.Longitude != null).ToList();

                    foreach (var provider in providerlist)
                    {
                        var data = new Listofmedicalcentre();

                        var hospitalLocation = new Location(Convert.ToDouble(provider.Latitude), Convert.ToDouble(provider.Longitude));

                        distance = Location.CalculateDistance(hospitalLocation, myLocation, DistanceUnits.Kilometers);

                        data.Longitude = provider.Longitude;

                        data.Latitude = provider.Latitude;

                        data.Location = provider.Location;

                        data.Facility_Contact = provider.Facility_Contact;

                        data.County = provider.County;

                        data.Region = provider.Region;

                        data.Working_Hours = provider.Working_Hours;

                        data.Physical_Address = provider.Physical_Address;

                        data.Physical_Address = provider.Physical_Address;

                        data.MO_CO = provider.MO_CO;

                        data.Pharmacy = provider.Pharmacy;

                        data.Laboratory = provider.Laboratory;

                        data.Optical = provider.Optical;

                        data.Dental = provider.Dental;

                        data.XRAY = provider.XRAY;

                        data.UltraSound = provider.UltraSound;

                        data.CTScan = provider.CTScan;

                        data.Physiotherapy = provider.Physiotherapy;

                        data.Distance = distance;

                        list.Add(data);
                    }

                    var newlits = list.Where(x => x.Distance <= 25).OrderBy(x => x.Distance).ToList();

                    if (ListOfMedicalCentre.Count() != 0)
                    {
                        ListOfMedicalCentre.Clear();
                    }
                    foreach (var item in newlits)
                    {
                        ListOfMedicalCentre.Add(item);
                    }
                    var list1 = new List<MapLocation>();

                    foreach (var item in newlits)
                    {
                        var newData = new MapLocation(

                        item.County,

                        item.Location,

                        new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)));

                        list1.Add(newData);
                    }

                    if (listofLocations.Count() != 0)
                    {
                        listofLocations.Clear();
                    }
                    foreach (var item in list1)
                    {
                        listofLocations.Add(item);
                    }
                    map.ItemsSource = listofLocations;

                    colection.ItemsSource = ListOfMedicalCentre;

                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(latitude, longitude), Distance.FromKilometers(10)));
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
    public async Task GetAllCentres()
    {
        try
        {
            Location myLocation = await Geolocation.GetLastKnownLocationAsync();

            if (myLocation == null)
            {
                await Application.Current.MainPage?.ShowPopupAsync(new InfoMessage());
                //await Shell.Current.GoToAsync(nameof(SignInPage2));
                return;
            }
            if (myLocation != null)
            {
                string CountyName = Preferences.Default.Get("CountyName", "Unknown");

                var list = new List<MapLocation>();

                var data = (await medicalCenteRepository.GetMedicalcentre()).Item2.listOfMedicalCentres;

                if (data == null)
                {
                    return;
                }


                if (ListOfMedicalCentre.Count() != 0)
                {
                    ListOfMedicalCentre.Clear();
                }
                foreach (var item in data)
                {
                    ListOfMedicalCentre.Add(item);
                }

                foreach (var item in data)
                {
                    var newData = new MapLocation(

                        item.County,

                        item.Location,

                        new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)));

                    list.Add(newData);
                }

                if (listofLocations.Count() != 0)
                {
                    listofLocations.Clear();
                }
                foreach (var item in list)
                {
                    listofLocations.Add(item);
                }
                map.ItemsSource = listofLocations;

                colection.ItemsSource = ListOfMedicalCentre;

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(myLocation.Latitude, myLocation.Longitude), Distance.FromKilometers(550)));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

            return;
        }
    }
    public async Task GetByCountyName()
    {
        try
        {
            Location myLocation = await Geolocation.GetLastKnownLocationAsync();

            if (myLocation == null)
            {
                await Application.Current.MainPage?.ShowPopupAsync(new InfoMessage());
                //await Shell.Current.GoToAsync(nameof(SignInPage2));
                return;
            }
            if (myLocation != null)
            {
                string CountyName = Preferences.Default.Get("CountyName", "Unknown");

                var list = new List<MapLocation>();

                var locatons = (await medicalCenteRepository.GetMedicalcentreByCountyName(CountyName)).Item2;

                if (locatons == null)
                {
                    return;
                }

                var providerlist = locatons.listOfMedicalCentres.Where(x => x.Latitude != null && x.Longitude != null).ToList();

                var data = providerlist.Where(x => x.County == CountyName);

                if (ListOfMedicalCentre.Count() != 0)
                {
                    ListOfMedicalCentre.Clear();
                }
                foreach (var item in data)
                {
                    ListOfMedicalCentre.Add(item);
                }

                foreach (var item in data)
                {
                    var newData = new MapLocation(

                        item.County,

                        item.Location,

                        new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)));

                    list.Add(newData);
                }

                if (listofLocations.Count() != 0)
                {
                    listofLocations.Clear();
                }
                foreach (var item in list)
                {
                    listofLocations.Add(item);
                }
                map.ItemsSource = listofLocations;

                colection.ItemsSource = ListOfMedicalCentre;

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(myLocation.Latitude, myLocation.Longitude), Distance.FromKilometers(450)));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

            return;
        }
    }
    public async Task<PermissionStatus> CheckAndRequestLocationPermission()
    {
        try
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)

                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            return status;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

            throw;
        }
    }

}