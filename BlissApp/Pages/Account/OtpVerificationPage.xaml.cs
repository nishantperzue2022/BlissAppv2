using BlissApp.ViewModels;

namespace BlissApp.Pages.Account;

public partial class OtpVerificationPage : ContentPage
{
    private System.Timers.Timer _otpTimer;

    private int _timeLeft = 30; // Timer duration in seconds

    private string _currentOtp = string.Empty;
    public OtpVerificationPage(AccountViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;

        txtNo1.Focus();

        _otpTimer = new System.Timers.Timer(1000); // Trigger every second

        _otpTimer.Elapsed += OnTimerElapsed;
    }

    protected override void OnAppearing()
    {
        ResendCode.IsVisible = false;

        RunOTPTimer();

        base.OnAppearing();
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

    private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (_timeLeft > 0)
        {
            _timeLeft--; // Decrease time left
                         // Update the UI on the main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = $"Resend code in {_timeLeft / 60:D2}:{_timeLeft % 60:D2}"; // Update timer display
            });
        }
        else
        {
            _otpTimer.Stop(); // Stop timer when expired

            MainThread.BeginInvokeOnMainThread(() =>
            {
                ResendCode.IsVisible = true;

                TimerLabel.IsVisible = false;

                Preferences.Default.Remove("otp");

                //TimerLabel.Text = "Expired"; // Indicate the OTP has expired
            });

            ClearInPuts();
        }
    }

    private void ClearInPuts()
    {
        txtNo1.Text = string.Empty; 
        txtNo2.Text = string.Empty; 
        txtNo3.Text = string.Empty; 
        txtNo4.Text = string.Empty; 
        txtNo5.Text = string.Empty; 
        txtNo6.Text = string.Empty;
        txtNo1.Focus();
    }

    protected void RunOTPTimer()
    {
        _timeLeft = 120; // Reset timer to 30 seconds

        TimerLabel.Text = $"Resend code in{_timeLeft:D2}:00"; // Display timer

        _otpTimer.Start(); // Start the countdown timer   

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        ResendCode.IsVisible = false;

        TimerLabel.IsVisible = true;

        AccountViewModel vm = new();

        await vm.ResendOTP();

        RunOTPTimer();
    }  
}

