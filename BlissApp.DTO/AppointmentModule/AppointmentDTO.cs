using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.AppointmentModule
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public int DepartmentId { get; set; }
        public int MedicalCentreId { get; set; }
        public string Relation { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ProposedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AccessToken { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PriorityTime { get; set; }
        public Guid PatientId { get; set; }
    }
}
