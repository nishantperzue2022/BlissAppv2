using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class OTPLoginPage : ContentPage
{
    AccountViewModel _vm;
    public OTPLoginPage()
	{
		InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new AccountViewModel();

        BindingContext = _vm;

        txtNo1.Focus();
    }

    private void txtNo1_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtNo1.Text.Trim();

        if (!string.IsNullOrEmpty(text))
        {
            txtNo2.Focus();
        }
        else
        {
            txtNo1.Unfocus();
        }

    }

    private void txtNo2_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtNo2.Text.Trim();

        if (!string.IsNullOrEmpty(text))
        {
            txtNo3.Focus();
        }
        else
        {
            txtNo2.Unfocus();
            txtNo1.Focus();
        }
    }

    private void txtNo3_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtNo3.Text.Trim();

        if (!string.IsNullOrEmpty(text))
        {
            txtNo4.Focus();
        }
        else
        {
            txtNo3.Unfocus();
            txtNo2.Focus();
        }
    }

    private void txtNo4_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtNo4.Text.Trim();

        if (!string.IsNullOrEmpty(text))
        {
            txtNo5.Focus();
        }
        else
        {
            txtNo4.Unfocus();
            txtNo3.Focus();
        }
    }

    private void txtNo5_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtNo5.Text.Trim();

        if (!string.IsNullOrEmpty(text))
        {
            txtNo6.Focus();
        }
        else
        {
            txtNo5.Unfocus();
            txtNo4.Focus();

        }
    }

    private void txtNo6_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtNo6.Text.Trim();

        if (!string.IsNullOrEmpty(text))
        {
            txtNo6.Focus();
        }
        else
        {
            txtNo5.Unfocus();
            txtNo5.Focus();
        }
    }
}