using API.Entities;

namespace API.Interfaces
{
    public interface IAtractieTuristicaRepository
    {
        void AdaugaAtractieTuristica(AtractieTuristica atractieTuristica);

        void AdaugaListaAtractiiTuristice(List<AtractieTuristica> listaAtractiiTuristice);

        void UpdateAtractieTuristica(AtractieTuristica atractieTuristica);

        AtractieTuristica GetAtractieTuristica(int atractieTuristicaId);
        Task<List<AtractieTuristica>> GetAll();
        List<AtractieTuristica> GetAllByCityId(int CityId);
        bool VerificaExistentaAtractieTuristica(string numeAtractie, int CityId);
    }
}