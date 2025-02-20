using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MedicalCentres;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace BlissApp.BLL.MedicalCenterModule
{
    public class MedicalCenteRepository : IMedicalCenteRepository
    {

        public async Task<Tuple<bool, MedicalcentreDTO, string>> GetMedicalcentre()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var memberNumber = userDetails.Email;

                var accessToken = userDetails.Access_token;

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/MedicalCentres/GetMedicalCentres");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<MedicalcentreDTO>(results);

                    return new Tuple<bool, MedicalcentreDTO, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, MedicalcentreDTO, string>(false, null, "There is no data");
                }

                return new Tuple<bool, MedicalcentreDTO, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, MedicalcentreDTO, string>(false, null, "Member details");
            }
        }
        public async Task<Tuple<bool, MedicalcentreDTO, string>> GetMedicalcentreByCountyName(string Name)
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var memberNumber = userDetails.Email;

                var accessToken = userDetails.Access_token;

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/MedicalCentres/GetByCountyName?Name=" + Name);

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<MedicalcentreDTO>(results);

                    return new Tuple<bool, MedicalcentreDTO, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, MedicalcentreDTO, string>(false, null, "There is no data");
                }

                return new Tuple<bool, MedicalcentreDTO, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, MedicalcentreDTO, string>(false, null, "Member details");
            }
        }
    }
}
