namespace BlissApp.DTO.AuthenticationModule
{

    public class Login
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Id { get; set; }
        public int Expires_in { get; set; }
        public int Creation_Time { get; set; }
        public int Expiration_Time { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string MemberName => Firstname + " " + Lastname;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string age { get; set; }
        public DateTime dob { get; set; }
        public string entryDate { get; set; }
        public string Password { get; set; }
        public string gender { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte IsEmailVerified { get; set; }
        public string NewDateOfBith
        {
            get
            {
                return dob.ToString("dddd, dd MMMM yyyy");


            }
        }
    }

}
