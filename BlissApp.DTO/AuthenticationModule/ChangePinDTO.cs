namespace BlissApp.DTO.AuthenticationModule
{
    public class ChangePinDTO
    {
        public Guid Id { get; set; }
        public string CurrentPin { get; set; }
        public string NewPin { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
