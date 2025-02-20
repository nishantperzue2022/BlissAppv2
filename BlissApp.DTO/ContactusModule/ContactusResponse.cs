namespace BlissApp.DTO.ContactusModule
{

    public class ContactusResponse
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string website { get; set; }
        public string linda { get; set; }
    }

}
