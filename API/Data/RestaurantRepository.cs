using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RestaurantRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaRestaurant(Restaurant Restaurant)
        {
            _context.Restaurante.Add(Restaurant);
        }

        public void AdaugaListaRestaurante(List<Restaurant> listaRestaurante)
        {
            _context.Restaurante.AddRange(listaRestaurante);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurante.Update(restaurant);
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
           return _context.Restaurante.Where(x=> x.Id == restaurantId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
        public List<Restaurant> GetAll()
        {
            return _context.Restaurante.Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public List<Restaurant> GetAllByCityId(int cityId)
        {
            return _context.Restaurante.Where(x=> x.CityId == cityId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public bool VerificaExistentaRestaurant(string numeRestaurant, int CityId)
        {
            return _context.Restaurante.Any(x=> x.Nume == numeRestaurant && x.CityId == CityId);
        }
        public Restaurant GetRestaurantByPhotoId(int photoId)
        {
           return _context.Restaurante.Where(x=> x.Photos.Any(y=> y.Id == photoId)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
    }
}
