using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MemberModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace BlissApp.BLL.MemberModule
{
    public class MemberRepository : IMemberRepository
    {

        public async Task<Tuple<bool, MemberResponse, string>> GetMember()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var MemberNo = userDetails.Email;



                string accessToken = userDetails.Access_token;

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Members/GetByMemberNo?SchemeId=" + MemberNo + "&MemberNo=" + MemberNo + "");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<MemberResponse>(results);

                    return new Tuple<bool, MemberResponse, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, MemberResponse, string>(false, null, "There is no data");
                }

                return new Tuple<bool, MemberResponse, string>(false, null, "There is no data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, MemberResponse, string>(false, null, "Member details");
            }
        }

    }
}
