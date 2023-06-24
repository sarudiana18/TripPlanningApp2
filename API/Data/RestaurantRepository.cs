using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public async Task<PagedList<RestaurantDto>> GetRestaurantsAsync(RestaurantFilterDto restaurantParams)
        {
            var restaurante = new List<Restaurant>();
             if(!restaurantParams.CityId.HasValue || restaurantParams.CityId.Value == 0){
                restaurante = this.GetAll();
            }
            else{
                restaurante = this.GetAllByCityId(restaurantParams.CityId.Value);
            } 
            var query = restaurante.AsQueryable();

            if( !string.IsNullOrEmpty(restaurantParams.Nume) && restaurantParams.Nume != "null"){
                query = query.Where(u => u.Nume.Contains(restaurantParams.Nume));
            }
            if( !string.IsNullOrEmpty(restaurantParams.Adresa) && restaurantParams.Adresa != "null"){
                query = query.Where(u => u.Adresa.Contains(restaurantParams.Adresa));
            }
            if( !string.IsNullOrEmpty(restaurantParams.Specific) && restaurantParams.Specific != "null"){
                query = query.Where(u => u.Specific.Contains(restaurantParams.Specific));
            }
            if(restaurantParams.Rating.HasValue){
                query = query.Where(u => u.Rating >= restaurantParams.Rating);
            }
            query = restaurantParams.SortField switch
            {
                "nume" => query.OrderBy(u => u.Nume),
                "rating" => query.OrderByDescending(u => u.Rating),
                _ => query.OrderByDescending(u => u.Nume)
            };
             var restauranteDto = _mapper.Map<IList<RestaurantDto>>(query);

            return await PagedList<RestaurantDto>.CreateAsync(
                restauranteDto.AsQueryable(), 
                restaurantParams.PageNumber, 
                restaurantParams.PageSize);

        }

        public void Delete(Restaurant obiect)
        {
            _context.Restaurante.Remove(obiect);
        }
    }
}
