using BlissApp.DTO.AuthenticationModule;

namespace BlissApp.BLL.Utils
{
    public static class OTPGenerator
    {
        public static string GenerateOTP(string phoneNumber)
        {
            try
            {

                char[] charArr = "0123456789".ToCharArray();

                string strrandom = string.Empty;

                Random objran = new();

                for (int i = 0; i < 6; i++)
                {
                    int pos = objran.Next(1, charArr.Length);

                    if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);

                    else i--;
                }

                var onetime_password = strrandom;

                OtpDTO otpDTO = new()
                {
                    OTP = onetime_password,

                    PhoneNumber = phoneNumber
                };

                return onetime_password;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;

            }
        }
    }
}
