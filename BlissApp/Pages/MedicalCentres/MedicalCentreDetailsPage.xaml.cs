using BlissApp.Control;
using BlissApp.ViewModels;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace BlissApp.Pages.MedicalCentres;

public partial class MedicalCentreDetailsPage : ContentPage
{
    CountyViewModel _vm;
	public MedicalCentreDetailsPage()
	{
		InitializeComponent();

        _vm = new CountyViewModel();

        BindingContext = _vm;

    }

    protected override async void OnAppearing()
    {
        try
        {
            ShowHospitalOnMap();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

            return;
        }
    }


    private async void ShowHospitalOnMap()
    {
        try
        {

            Location myLocation = await Geolocation.GetLastKnownLocationAsync();

            string longitude = Preferences.Default.Get("longitude", "0");

            string latitude = Preferences.Default.Get("latitude", "0");

            string county = Preferences.Default.Get("county", "0");

            string providerName = Preferences.Default.Get("providerName", "0");

            map.Pins.Clear();

            Location position = new Location(Convert.ToDouble(latitude), Convert.ToDouble(longitude));

            //Pin pin = new Pin()
            //{
            //    Type = PinType.Place,
            //    Label = hospital.name,
            //    Address = "",
            //    Location = position,
            //    Rotation = 33.3f,
            //    Tag = hospital.name,

            //};

            Pin pin = new Pin
            {
                Label = providerName,

                Address = county,

                Type = PinType.Place,

                Location = position,
            };

            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(Convert.ToDouble(latitude), Convert.ToDouble(longitude)), Distance.FromKilometers(1)));

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
    }
}