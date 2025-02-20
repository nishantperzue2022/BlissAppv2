namespace BlissApp.DTO.OfferModule
{
    public class OfferDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }
}
