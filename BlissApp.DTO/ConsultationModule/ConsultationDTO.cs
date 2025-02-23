namespace BlissApp.DTO.ConsultationModule
{
  public  class ConsultationDTO
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ConsultationsDate { get; set; }
        public int HospitalId { get; set; }
        public byte Status { get; set; }
    }
}
