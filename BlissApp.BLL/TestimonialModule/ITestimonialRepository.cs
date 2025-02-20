using BlissApp.DTO.TestimonialModule;

namespace BlissApp.BLL.TestimonialModule
{
    public interface ITestimonialRepository
    {
        Task<Tuple<bool, TestimonialResponse, string>> GetTestimonials();
    }
}