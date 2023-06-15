using API.Entities;

namespace API.Interfaces
{
    public interface ICountryRepository
    {
        void AdaugaCountry(Country Country);

        void AdaugaListaCountries(List<Country> listaCountries);

        void UpdateCountry(Country Country);

        Country GetCountry(Country Country);
        List<Country> GetAll();
    }
}