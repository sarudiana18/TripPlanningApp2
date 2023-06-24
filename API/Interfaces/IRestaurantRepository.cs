using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IRestaurantRepository
    {
        void AdaugaRestaurant(Restaurant Restaurant);

        void AdaugaListaRestaurante(List<Restaurant> listaRestaurante);

        void UpdateRestaurant(Restaurant Restaurant);

        Restaurant GetRestaurant(int restaurantId);
        List<Restaurant> GetAll();
        List<Restaurant> GetAllByCityId(int cityId);
        bool VerificaExistentaRestaurant(string numeRestaurant, int CityId);
        Restaurant GetRestaurantByPhotoId(int photoId);
        Task<PagedList<RestaurantDto>> GetRestaurantsAsync(RestaurantFilterDto restaurantParams);
        void Delete(Restaurant obiect);
    }
    
}