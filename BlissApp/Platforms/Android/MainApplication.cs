using Android.App;
using Android.Runtime;


namespace BlissApp
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY", Value = "AIzaSyCsQ_A63oZz9dkeyMJEgsVkpq0iYvDKX10")]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
