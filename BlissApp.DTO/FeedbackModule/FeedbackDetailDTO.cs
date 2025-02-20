using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.FeedbackModule
{
    public class FeedbackDetailDTO
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid? PatientId { get; set; }
        public int DepartmentId { get; set; }
        public int MedicalCentreId { get; set; }
        public string? Description { get; set; }
        public string? DepartmentName { get; set; }
        public string? MedicalCentreName { get; set; }
        public string? PatientName { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public string? ResolvedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte Status { get; set; }
        public string NewDateOfVisit
        {
            get
            {
                return DateOfVisit.ToString("ddd, dd MMM yyy");
            }
        }
    }
}
