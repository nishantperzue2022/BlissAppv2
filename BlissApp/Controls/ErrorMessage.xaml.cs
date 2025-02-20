using CommunityToolkit.Maui.Views;

namespace BlissApp.Control;

public partial class ErrorMessage : Popup
{

	public ErrorMessage(string title, string message, string okButtonText = "Ok", string cancelButtonText = "Cancel", bool isInfoPopup = true)
    {
        InitializeComponent();
        lblMessage.Text = message;
        btnCancel.Text = cancelButtonText;
        btnOk.Text = okButtonText;

        if (isInfoPopup)
        {
            btnCancel.IsVisible = false;
        }
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