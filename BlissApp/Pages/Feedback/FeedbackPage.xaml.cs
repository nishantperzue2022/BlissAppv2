using BlissApp.ViewModels;

namespace BlissApp.Pages.Feedback;

public partial class FeedbackPage : ContentPage
{
    public FeedbackPage(FeedbackViewModel vm)
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        BindingContext = vm;

      
    }

    protected override void OnAppearing()
    {
        IsOtherMembersFeed.IsVisible = false;

        selfFeed.IsChecked = true;
    }

    private void CheckBox_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {

        if (selfFeed.IsChecked == true)
        {
            IsOtherMembersFeed.IsVisible = false;

            txtRelationFeed.Text = selfFeed.Text;
        }
        else
        {
            IsOtherMembersFeed.IsVisible = true;

            txtRelationFeed.Text = otherFeed.Text;

        }
    }
}