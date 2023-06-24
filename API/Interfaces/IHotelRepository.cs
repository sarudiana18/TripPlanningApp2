using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IHotelRepository
    {
        void AdaugaHotel(Hotel Hotel);

        void AdaugaListaHoteluri(List<Hotel> listaHoteluri);

        void UpdateHotel(Hotel Hotel);

        Hotel GetHotel(int hotelId);
        List<Hotel> GetAll();
        List<Hotel> GetAllByCityId(int cityId);
        bool VerificaExistentaHotel(string numeHotel, int CityId);
        List<Hotel> GetAllByCityIdAndBugetAndNrNoptiAndNrPersoane(int cityId, int buget, int nrNopti, int nrPersoane);

        Hotel GetHotelByPhotoId(int photoId);
        Task<PagedList<HotelDto>> GetHotelsAsync(HotelFilterDto hotelParams);
        void Delete(Hotel obiect);
    }
    
}