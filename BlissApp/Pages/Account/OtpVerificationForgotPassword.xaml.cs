using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class OtpVerificationForgotPassword : ContentPage
{

    private System.Timers.Timer _otpTimer;

    private int _timeLeft = 30; // Timer duration in seconds

    private string _currentOtp = string.Empty;

    ProfileViewModel _vm;
    public OtpVerificationForgotPassword()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _vm = new ProfileViewModel();

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

    protected override void OnAppearing()
    {
        ResendCode.IsVisible = false;

        RunOTPTimer();

        base.OnAppearing();
    }

    protected void RunOTPTimer()
    {
        _timeLeft = 120; // Reset timer to 30 seconds

        TimerLabel.Text = $"Resend code in{_timeLeft:D2}:00"; // Display timer

        _otpTimer.Start(); // Start the countdown timer   
    }

}