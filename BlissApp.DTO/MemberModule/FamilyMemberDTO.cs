namespace BlissApp.DTO.MemberModule
{
    public class FamilyMemberDTO
    {
        public Guid Id { get; set; }
        public Guid PrincipalMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Relation { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewDateOfBith
        {
            get
            {
                return DateOfBirth.ToString("dddd, dd MMMM yyyy");
            }
        }
        public string NewCreateDate
        {
            get
            {
                return CreateDate.ToString("dddd, dd MMMM yyyy");
            }
        }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Subtract(DateOfBirth).Days;

                age = age / 365;

                return age;
            }
        }
    }
}
