using BlissApp.DTO.HospitalVisitModule;
using BlissApp.DTO.MedicalCentres;

namespace BlissApp.BLL.MedicalCenterModule
{
    public interface IMedicalCenteRepository
    {
        Task<Tuple<bool, MedicalcentreDTO, string>> GetMedicalcentre();
        Task<Tuple<bool, MedicalcentreDTO, string>> GetMedicalcentreByCountyName(string Name);
    }
}