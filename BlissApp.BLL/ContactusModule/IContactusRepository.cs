using BlissApp.DTO.ContactusModule;

namespace BlissApp.BLL.ContactusModule
{
    public interface IContactusRepository
    {
        Task<Tuple<bool, ContactusResponse, string>> GetContactDetails();
    }
}