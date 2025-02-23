using BlissApp.DTO.ConsultationModule;
using BlissApp.DTO.OrderModule;

namespace BlissApp.BLL.ConsultationModule
{
    public interface IConsultationRepository
    {
        Task<bool> Create(ConsultationDTO orderDTO, string AccessToken);
        Task<Tuple<bool, OrderListResponse, string>> GetMyConsultations();
    }
}