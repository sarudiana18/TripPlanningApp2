using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaReview(Review City)
        {
            _context.Reviews.Add(City);
        }

        public void AdaugaListaReviews(List<Review> listaReviews)
        {
            _context.Reviews.AddRange(listaReviews);
        }

        public void UpdateReview(Review Review)
        {
            _context.Reviews.Update(Review);
        }

        public Review GetReview(Review Review)
        {
           return _context.Reviews.FirstOrDefault(x=> x.Id == Review.Id);
        }
        public List<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }
        public List<Review> GetAllByHotelId(int hotelId)
        {
            return _context.Reviews.Where(x=> x.HotelId == hotelId).Include(x=> x.CreatedByUser).ToList();
        }
        public List<Review> GetAllByParcId(int parcId)
        {
            return _context.Reviews.Where(x=> x.ParcId == parcId).Include(x=> x.CreatedByUser).ToList();
        }
        public List<Review> GetAllByRestaurantId(int restaurantId)
        {
            return _context.Reviews.Where(x=> x.RestaurantId == restaurantId).Include(x=> x.CreatedByUser).ToList();
        }
    }
}
