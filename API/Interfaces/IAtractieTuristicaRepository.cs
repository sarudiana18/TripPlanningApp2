using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IAtractieTuristicaRepository
    {
        
        Task<AtractieTuristica> AdaugaAtractieTuristica(AtractieTuristica atractieTuristica);

        void AdaugaListaAtractiiTuristice(List<AtractieTuristica> listaAtractiiTuristice);

        void UpdateAtractieTuristica(AtractieTuristica atractieTuristica);

        AtractieTuristica GetAtractieTuristica(int atractieTuristicaId);
        Task<List<AtractieTuristica>> GetAll();
        List<AtractieTuristica> GetAllByCityId(int CityId);
        bool VerificaExistentaAtractieTuristica(string numeAtractie, int CityId);
        AtractieTuristica GetAtractieByPhotoId(int photoId);
        Task<PagedList<AtractieTuristicaDto>> GetAtractiiAsync(AtractieTuristicaFilterDto atractieParams);
        void Delete(AtractieTuristica atractieTuristica);

    }
}