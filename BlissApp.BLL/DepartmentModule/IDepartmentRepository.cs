using BlissApp.DTO.DepartmentModule;
using BlissApp.DTO.MemberModule;

namespace BlissApp.BLL.DepartmentModule
{
    public interface IDepartmentRepository
    {
        Task<Tuple<bool, DepartmentResponse, string>> GetDepartments();
    }
}