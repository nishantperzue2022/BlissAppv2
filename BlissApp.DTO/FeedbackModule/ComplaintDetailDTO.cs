namespace BlissApp.DTO.FeedbackModule
{
    public class ComplaintDetailDTO
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public int DepartmentId { get; set; }
        public int MedicalCentreId { get; set; }
        public string? Description { get; set; }
        public string? DepartmentName { get; set; }
        public string MedicalCentreName { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public string ResolvedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte Status { get; set; }
    }
}
