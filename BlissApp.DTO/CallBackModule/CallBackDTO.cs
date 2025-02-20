namespace BlissApp.DTO.CallBackModule
{
    public class CallBackDTO
    {
        public string Id { get; set; }
        public Guid? MemberId { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberName { get; set; }
        public string CallBackReasons { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AccessToken { get; set; }
        public string NewCreateDate
        {
            get
            {
                return CreateDate.ToString("ddd, dd MMM yyy HH:mm:ss");
            }
        }
    }
}
