using BlissApp.DTO.MemberModule;

namespace BlissApp.BLL.FamilyMemberModule
{
    public interface IFamilyMemberRepository
    {
        Task<bool> Create(FamilyMemberDTO complaintDTO);
        Task<bool> DeleteMember(FamilyMemberDTO familyMemberDTO);
        Task<bool> Edit(FamilyMemberDTO complaintDTO);
        Task<Tuple<bool, FamilyMemberResponse, string>> GetFamilyMember();
    }
}