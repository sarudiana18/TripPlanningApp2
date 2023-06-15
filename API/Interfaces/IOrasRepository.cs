using API.Entities;

namespace API.Interfaces
{
    public interface ICityRepository
    {
        void AdaugaCity(City City);

        void AdaugaListaCities(List<City> listaCities);

        void UpdateCity(City City);

        City GetCity(City City);
        List<City> GetAll();
        List<City> GetAllByCountryId(int countryId);
        List<City> GetAllByStateId(int stateId);
    }
}