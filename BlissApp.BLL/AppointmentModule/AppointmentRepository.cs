using BlissApp.BLL.Utils;
using BlissApp.DTO.AppointmentModule;
using BlissApp.DTO.AuthenticationModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace BlissApp.BLL.AppointmentModule
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public async Task<Tuple<bool, AppointmentResponse, string>> GetAppointments()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var MemberId = userDetails.Id;

                var accessToken = userDetails.Access_token;

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Appointments/GetAppointments?MemberId=" + MemberId + "");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<AppointmentResponse>(results);

                    return new Tuple<bool, AppointmentResponse, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, AppointmentResponse, string>(false, null, "There is no data");
                }

                return new Tuple<bool, AppointmentResponse, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, AppointmentResponse, string>(false, null, "Member details");
            }
        }
        public async Task<bool> Create(AppointmentDTO appointmentDTO)
        {
            try
            {
                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(appointmentDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appointmentDTO.AccessToken);

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Appointments/Create", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    var result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #                

                    if (result.Status == true)
                    {
                        return true;
                    }
                    if (result.Status == false)
                    {
                        return false;
                    }
                }
                if (response.IsSuccessStatusCode == false)
                {
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
