using BlissApp.ViewModels;

namespace BlissApp.Pages.Feedback;

public partial class FeedbackHistoryPage : ContentPage
{
	public FeedbackHistoryPage(ComplaintDetailViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}