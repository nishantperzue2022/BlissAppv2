namespace BlissApp.DTO.FeedbackModule
{
    public class FeedbackDTO
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid PatientId { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int MedicalCentreId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string? MemberName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Clinic { get; set; }
        public string NewDateOfVisit
        {
            get
            {
                return DateOfVisit.ToString("ddd, dd MMM yyy");
            }
        }
    }
}
