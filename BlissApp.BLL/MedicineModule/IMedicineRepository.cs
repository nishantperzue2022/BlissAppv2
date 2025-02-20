using BlissApp.DTO.DepartmentModule;
using BlissApp.DTO.MedicineModule;

namespace BlissApp.BLL.MedicineModule
{
    public interface IMedicineRepository
    {
        Task<Tuple<bool, MedicineResponse, string>> GetMedicine();

    }
}