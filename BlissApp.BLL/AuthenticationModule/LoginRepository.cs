using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MemberModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using static System.Net.WebRequestMethods;
namespace BlissApp.BLL.AuthenticationModule
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<Login> Login(string username, string password)
        {
            try
            {
                var client = new HttpClient();

                var login = new UserInfo()
                {
                    PhoneNumber = username.Trim(),

                    Password = password.Trim(),
                };

                var json = JsonConvert.SerializeObject(login);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    var result = JsonConvert.DeserializeObject<Login>(JsonResult); // deserialize json to c #

                    result.Password = password;

                    return await Task.FromResult(result);
                }

                if (response.IsSuccessStatusCode == false)
                {


                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<Tuple<bool, Response, string>> CreateAccount(AuthMemberDTO authMemberDTO)
        {
            try
            {
                var client = new HttpClient();

                Response result = new Response();

                var json = JsonConvert.SerializeObject(authMemberDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/CreateAccount", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #				

                    return new Tuple<bool, Response, string>(true, result, result.Message);

                }
                if (response.IsSuccessStatusCode == false)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #	

                    return new Tuple<bool, Response, string>(false, null, result.Message);

                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<Tuple<bool, Response, string>> UpdateInformation(AuthMemberDTO authMemberDTO)
        {
            try
            {
                var client = new HttpClient();

                Response result = new Response();

                var json = JsonConvert.SerializeObject(authMemberDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/UpdateDetails", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #				

                    return new Tuple<bool, Response, string>(true, result, result.Message);

                }
                if (response.IsSuccessStatusCode == false)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #	

                    return new Tuple<bool, Response, string>(false, null, result.Message);

                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<Tuple<bool, Response, string>> UpdateAccount(AuthMemberDTO authMemberDTO)
        {
            try
            {
                var client = new HttpClient();

                Response result = new Response();

                var json = JsonConvert.SerializeObject(authMemberDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/UpdateAccount", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #				

                    return new Tuple<bool, Response, string>(true, result, result.Message);

                }
                if (response.IsSuccessStatusCode == false)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #	

                    return new Tuple<bool, Response, string>(false, null, result.Message);

                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<bool> FortgotPin(ForgetPIN authMemberDTO)
        {
            try
            {
                var client = new HttpClient();

                Response result = new Response();

                var json = JsonConvert.SerializeObject(authMemberDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/ForgotPin", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #				

                    return true;
                }
                if (response.IsSuccessStatusCode == false)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    result = JsonConvert.DeserializeObject<Response>(JsonResult); // deserialize json to c #	

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
        public async Task<bool> DeleteAccount()
        {
            try
            {
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                var phoneNumber = userDetails.PhoneNumber.Trim();

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/DeleteAccount?PhoneNumber=" + phoneNumber + "", null);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    var results = JsonConvert.DeserializeObject<Response>(data);

                    if (results.Status == true)
                    {
                        return true;
                    }

                    return false;
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return false;
                }

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return false;
            }
        }

        public async Task<Login> SendOTP(OtpDTO otpDTO)
        {
            try
            {
                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(otpDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/OTPAuthentication", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    var result = JsonConvert.DeserializeObject<Login>(JsonResult); // deserialize json to c #                  

                    return await Task.FromResult(result);
                }
                if (response.IsSuccessStatusCode == false)
                {


                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ChangePinDTO> ChangePin(ChangePinDTO changePinDTO)
        {
            try
            {
                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(changePinDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/ChangePin", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    var result = JsonConvert.DeserializeObject<ChangePinDTO>(JsonResult); // deserialize json to c #

                    ///login = await response.Content.ReadFromJsonAsync<UserInfo>();

                    return result;
                }
                if (response.IsSuccessStatusCode == false)
                {


                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<Tuple<bool, MemberDTO, string>> AuthenticateMember(string MemberNumber, string SchemeId)
        {
            try
            {
                var MemberNo = MemberNumber;

                int schemeId = Convert.ToInt32(SchemeId);

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Account/GetMember?SchemeId=" + schemeId + "&MemberNo=" + MemberNo + "");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var members = JsonConvert.DeserializeObject<UserResponse>(results);

                    switch (members.status)
                    {
                        case true:
                            return new Tuple<bool, MemberDTO, string>(true, members.message, "Member details");
                        default:
                            return new Tuple<bool, MemberDTO, string>(false, members.message, "Member details");
                    }
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, MemberDTO, string>(false, null, "There is no data");
                }

                return new Tuple<bool, MemberDTO, string>(false, null, "There is no data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, MemberDTO, string>(false, null, "Member details");
            }
        }

        public async Task<VerifyEmailDTO> VerifyEmail(VerifyEmailDTO verifyEmailDTO)
        {
            try
            {
                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(verifyEmailDTO);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(MaklAPI.ApiUrl + "api/Account/VerifyEmail", content);

                if (response.IsSuccessStatusCode)
                {
                    var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

                    var result = JsonConvert.DeserializeObject<VerifyEmailDTO>(JsonResult); // deserialize json to c #

                    ///login = await response.Content.ReadFromJsonAsync<UserInfo>();

                    return result;
                }
                if (response.IsSuccessStatusCode == false)
                {


                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<Tuple<bool, VerifyAccountResponse, string>> VerifyAccountAccount(Guid Id)
        {
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(MaklAPI.ApiUrl + "api/Account/VerifyAccountAccount?Id=" + Id + "");

                if (response.IsSuccessStatusCode)
                {
                    string results = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<VerifyAccountResponse>(results);

                    if (apiResponse != null)
                    {
                        return new Tuple<bool, VerifyAccountResponse, string>(true, apiResponse, "Data");

                    }

                    return new Tuple<bool, VerifyAccountResponse, string>(false, null, "Unable to process your request at this time,Please try again");
                }

                if (response.IsSuccessStatusCode == false)
                {
                    return new Tuple<bool, VerifyAccountResponse, string>(false, null, "Unable to process your request at this time,Please try again");
                }

                return new Tuple<bool, VerifyAccountResponse, string>(false, null, "Unable to process your request at this time,Please try again");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Tuple<bool, VerifyAccountResponse, string>(false, null, "Unable to process your request at this time,Please try again");
            }
        }
    }
}
