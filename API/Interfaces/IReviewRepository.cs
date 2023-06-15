using API.Entities;

namespace API.Interfaces
{
    public interface IReviewRepository
    {
        void AdaugaReview(Review City);

        void AdaugaListaReviews(List<Review> listaReviews);

        void UpdateReview(Review City);

        Review GetReview(Review City);
        List<Review> GetAll();
        List<Review> GetAllByHotelId(int hotelId);
    }
    
}