using CommunityToolkit.Maui.Views;

namespace BlissApp.Controls;

public partial class OfferPopup : Popup
{
	public OfferPopup()
	{
		InitializeComponent();
	}
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            Close(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
    }
}