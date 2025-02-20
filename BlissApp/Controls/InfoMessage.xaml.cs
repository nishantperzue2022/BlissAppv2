
using CommunityToolkit.Maui.Views;


namespace BlissApp.Control;

public partial class InfoMessage : Popup
{
    public InfoMessage()
    {
        InitializeComponent();

    }
    public InfoMessage( string okButtonText = "Ok",  bool isInfoPopup = true)
    {
        InitializeComponent();
        //lblMessage.Text = message;
        //btnCancel.Text = cancelButtonText;
        btnOk.Text = okButtonText;

    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Close(false);
    }

    private void btnOk_Clicked(object sender, EventArgs e)
    {
        Close(false);

    }
}