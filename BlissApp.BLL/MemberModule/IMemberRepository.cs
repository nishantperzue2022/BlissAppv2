using BlissApp.DTO.MemberModule;


namespace BlissApp.BLL.MemberModule
{
    public interface IMemberRepository
    {
        Task<Tuple<bool, MemberResponse, string>> GetMember();

    }
}