using BlissApp.DTO.CallBackModule;

namespace BlissApp.BLL.CallBackModule
{
    public interface ICallBackRepository
    {
        Task<bool> Create(CallBackDTO callBackDTO);
        Task<bool> DeleteCallback(CallBackDTO callBackDTO);
        Task<Tuple<bool, CallBackRequestResponse, string>> GetHistory();
    }
}