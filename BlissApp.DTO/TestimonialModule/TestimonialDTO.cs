namespace BlissApp.DTO.TestimonialModule
{
    public class TestimonialDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }
        public string? PatientName => FirstName + " " + LastName;
        public DateTime? CreateDate { get; set; }
        public byte Status { get; set; }
        public string GenderImage
        {
            get
            {
                if (Gender == "Female")
                {
                    return "Female";
                }
                else if (Gender == "Male")
                {
                    return "Male";
                }
                else
                {
                    return "Male";
                }
            }
        }



    }
}
