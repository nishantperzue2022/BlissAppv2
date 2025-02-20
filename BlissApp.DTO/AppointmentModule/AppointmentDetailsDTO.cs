using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.AppointmentModule
{
    public class AppointmentDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? PatientId { get; set; }
        public int DepartmentId { get; set; }
        public int MedicalCentreId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime ProposedDate { get; set; }
        public string? PriorityTime { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Relation { get; set; }
        public string? Gender { get; set; }
        public string? MedicalCentre { get; set; }
        public string? DepartmentName { get; set; }
        public string NewProposedDate
        {
            get
            {
                return ProposedDate.ToString("dddd, dd MMMM yyyy");

            }
        }

    }
}
