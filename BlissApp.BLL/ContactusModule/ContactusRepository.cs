using BlissApp.BLL.Utils;
using BlissApp.DTO.ContactusModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BlissApp.BLL.ContactusModule
{
    public class ContactusRepository : IContactusRepository
    {
        public async Task<Tuple<bool, ContactusResponse, string>> GetContactDetails()
        {
            try
            {

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Contacts/GetContactDetails");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<ContactusResponse>(results);

                    return new Tuple<bool, ContactusResponse, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, ContactusResponse, string>(false, null, "There is no data");
                }

                return new Tuple<bool, ContactusResponse, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, ContactusResponse, string>(false, null, "Member details");
            }
        }

    }
}
