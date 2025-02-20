namespace BlissApp.DTO.AuthenticationModule
{
    public class AuthMemberDTO
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string? County { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? BloodGroup { get; set; }
    }
}
