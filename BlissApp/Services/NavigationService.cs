namespace BlissApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;

        public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task NavigateToAsync(string pageKey)
        {
            // Implement navigation logic
        }

        public async Task NavigateBackAsync()
        {
            // Handle back navigation
            await _navigation.PopAsync();
            // Notify when back navigation happens
            NavigatedBack?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler NavigatedBack;
    }

}
