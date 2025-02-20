namespace BlissApp.DTO.OrderModule
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid MemberID { get; set; }
        public DateTime OrderDate { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string? MedicineName { get; set; }
        public string? AccessToken { get; set; }
        public string? Quantity { get; set; }
        public string? PickupLocation { get; set; }
    }
}
