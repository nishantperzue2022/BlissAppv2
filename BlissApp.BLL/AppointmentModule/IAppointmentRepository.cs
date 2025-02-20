using BlissApp.DTO.AppointmentModule;
using BlissApp.DTO.FindHospitalModule;

namespace BlissApp.BLL.AppointmentModule
{
    public interface IAppointmentRepository
    {
        Task<bool> Create(AppointmentDTO appointmentDTO);
        Task<Tuple<bool, AppointmentResponse, string>> GetAppointments();
    }
}