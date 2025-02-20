using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.DepartmentModule;
using BlissApp.DTO.MedicineModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace BlissApp.BLL.MedicineModule
{
    public class MedicineRepository : IMedicineRepository
    {
        public async Task<Tuple<bool, MedicineResponse, string>> GetMedicine()
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

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Medicine/GetMedicine");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<MedicineResponse>(results);

                    return new Tuple<bool, MedicineResponse, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, MedicineResponse, string>(false, null, "There is no data");
                }
                return new Tuple<bool, MedicineResponse, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, MedicineResponse, string>(false, null, "Member details");
            }
        }

    }
}
