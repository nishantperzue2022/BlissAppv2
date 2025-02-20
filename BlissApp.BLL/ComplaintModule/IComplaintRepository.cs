using BlissApp.DTO.FeedbackModule;

namespace BlissApp.BLL.ComplaintModule
{
    public interface IComplaintRepository
    {
        Task<bool> Create(FeedbackDTO complaintDTO);
        Task<bool> DeleteComplaint(FeedbackDTO complaintDTO);
        Task<Tuple<bool, FeedbackResponse, string>> GetHistory();
    }
}