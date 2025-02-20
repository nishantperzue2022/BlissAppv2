using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.FeedbackModule;
using BlissApp.DTO.OfferModule;
using BlissApp.DTO.OrderModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace BlissApp.BLL.OrderModule
{
    public class OrderRepository : IOrderRepository
    {

        public async Task<bool> Create(List<OrderDTO> orderDTO,string AccessToken)
        {
            try
            {          
                var client = new HttpClient();

                string json = JsonConvert.SerializeObject(orderDTO, Formatting.Indented);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Orders/Create", content);

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
        public async Task<bool> DeleteOrder(OrderDTO orderDTO)
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);             

                var accessToken = userDetails.Access_token;

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(orderDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Orders/DeleteOrder", content);

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

        public async Task<bool> DeleteAllOrder()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                OrderDTO orderDTO = new();

                orderDTO.MemberID = Guid.Parse(userDetails.Id);

                var accessToken = userDetails.Access_token;

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(orderDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.PostAsync(MaklAPI.ApiUrl + "api/Orders/DeleteAllOrders?MemberId=" + orderDTO.MemberID + "",null);

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
        
        public async Task<bool> SubmitOrder()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                OrderDTO orderDTO = new();

                orderDTO.MemberID = Guid.Parse(userDetails.Id);

                var accessToken = userDetails.Access_token;

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(orderDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.PostAsync(MaklAPI.ApiUrl + "api/Orders/SubmitOrders?MemberId=" + orderDTO.MemberID + "",null);

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
        public async Task<Tuple<bool, OrderListResponse, string>> GetMyOrders()
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

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Orders/GetOrders?MemberId=" + MemberId + "");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<OrderListResponse>(results);

                    return new Tuple<bool, OrderListResponse, string>(true, members, "Member details");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, OrderListResponse, string>(false, null, "There is no data");
                }

                return new Tuple<bool, OrderListResponse, string>(false, null, "There is no data");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, OrderListResponse, string>(false, null, "Member details");
            }
        }

    }
}
