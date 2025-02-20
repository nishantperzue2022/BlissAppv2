using BlissApp.DTO.OfferModule;
using BlissApp.DTO.TestimonialModule;

namespace BlissApp.BLL.OfferModule
{
    public interface IOfferRepository
    {
        Task<Tuple<bool, OfferResponse, string>> GetTestimonials();
    }
}