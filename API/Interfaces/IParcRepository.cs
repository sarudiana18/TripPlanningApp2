using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IParcRepository
    {
        void AdaugaParc(Parc Parc);

        void AdaugaListaParcuri(List<Parc> listaParcuri);

        void UpdateParc(Parc Parc);

        Parc GetParc(int parcId);
        List<Parc> GetAll();
        List<Parc> GetAllByCityId(int cityId);
        bool VerificaExistentaParc(string numeParc, int CityId);
        Parc GetParcByPhotoId(int photoId);
        Task<PagedList<ParcDto>> GetParcuriAsync(ParcFilterDto parcParams);
        void Delete(Parc obiect);
    }
    
}