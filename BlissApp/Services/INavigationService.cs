namespace BlissApp.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string pageKey);
        Task NavigateBackAsync();
        event EventHandler NavigatedBack;
    }
}