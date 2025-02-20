using CommunityToolkit.Maui.Views;
using BlissApp.BLL.AuthenticationModule;
using BlissApp.Control;
using BlissApp.DTO.AuthenticationModule;
using Newtonsoft.Json;

namespace BlissApp.Utility
{
    public static class TokenValidator
    {
        static readonly ILoginRepository loginRepository = new LoginRepository();

        public static async Task<bool> CheckTokenValidity()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                if (logindetails != "0")
                {
                    var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                    var expirationTime = userDetails.Expiration_Time;

                    Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                    var currentTime = Preferences.Get("currentTime", 0);

                    if (expirationTime < currentTime)
                    {
                        var results = await loginRepository.Login(userDetails.Email, userDetails.Password);

                        if (results.Status == true)
                        {
                            if (Preferences.ContainsKey(nameof(App.UserInfo)))
                            {
                                Preferences.Remove(nameof(App.UserInfo));
                            }
                            string userdetails = JsonConvert.SerializeObject(results);

                            Preferences.Set(nameof(App.UserInfo), userdetails);

                            Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                            App.UserInfo = results;

                            return true;
                        }
                        if (results.Status == false)
                        {
                            await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Something went wrong. Please login and try again"));

                            return true;
                        }
                      
                    }

                  
                }

                return false;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Something went wrong. Please login and try again"));

                return false;
            }
        }
    }
}
