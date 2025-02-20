using BlissApp.DTO.FeedbackModule;
using BlissApp.DTO.OfferModule;
using BlissApp.DTO.OrderModule;

namespace BlissApp.BLL.OrderModule
{
    public interface IOrderRepository
    {
        Task<bool> Create(List<OrderDTO> orderDTO, string AccessToken);
        Task<bool> DeleteAllOrder();
        Task<bool> DeleteOrder(OrderDTO orderDTO);
        Task<Tuple<bool, OrderListResponse, string>> GetMyOrders();
        Task<bool> SubmitOrder();
    }
}