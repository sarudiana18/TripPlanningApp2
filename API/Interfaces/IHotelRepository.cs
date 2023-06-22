using API.Entities;

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
        public List<Hotel> GetAllByCityIdAndBugetAndNrNoptiAndNrPersoane(int cityId, int buget, int nrNopti, int nrPersoane);

        Hotel GetHotelByPhotoId(int photoId);
    }
    
}