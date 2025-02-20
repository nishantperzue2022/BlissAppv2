using BlissApp.BLL.Utils;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MemberModule;

namespace BlissApp.BLL.AuthenticationModule
{
    public interface ILoginRepository
    {
        Task<Tuple<bool, MemberDTO, string>> AuthenticateMember(string MemberNumber, string SchemeId);
        Task<ChangePinDTO> ChangePin(ChangePinDTO changePinDTO);
        Task<Tuple<bool, Response, string>> CreateAccount(AuthMemberDTO authMemberDTO);
        Task<bool> FortgotPin(ForgetPIN authMemberDTO);
        Task<Login> Login(string username, string password);
        Task<Login> SendOTP(OtpDTO otpDTO);
        Task<Tuple<bool, Response, string>> UpdateInformation(AuthMemberDTO authMemberDTO);
        Task<Tuple<bool, Response, string>> UpdateAccount(AuthMemberDTO authMemberDTO);
        Task<Tuple<bool, VerifyAccountResponse, string>> VerifyAccountAccount(Guid Id);
        Task<bool> DeleteAccount();
        Task<VerifyEmailDTO> VerifyEmail(VerifyEmailDTO verifyEmailDTO);
    }
}