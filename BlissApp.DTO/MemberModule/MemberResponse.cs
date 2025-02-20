namespace BlissApp.DTO.MemberModule
{

    public class MemberResponse
    {
        public string message { get; set; }
        public MemberDTO[] listOfMembers { get; set; }
        public bool status { get; set; }
    }

}
