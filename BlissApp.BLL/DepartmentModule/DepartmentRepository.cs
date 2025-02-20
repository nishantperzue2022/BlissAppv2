using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.DepartmentModule;
using BlissApp.DTO.MemberModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace BlissApp.BLL.DepartmentModule
{
    public class DepartmentRepository : IDepartmentRepository
    {

        public async Task<Tuple<bool, DepartmentResponse, string>> GetDepartments()
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

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Departments/GetDepartment");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<DepartmentResponse>(results);

                    return new Tuple<bool, DepartmentResponse, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, DepartmentResponse, string>(false, null, "There is no data");
                }

                return new Tuple<bool, DepartmentResponse, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, DepartmentResponse, string>(false, null, "Member details");
            }
        }


    }
}
