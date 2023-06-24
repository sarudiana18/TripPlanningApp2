using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    internal class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CountryRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaCountry(Country Country)
        {
            _context.Countries.Add(Country);
        }

        public void AdaugaListaCountries(List<Country> listaCountries)
        {
            _context.Countries.AddRange(listaCountries);
        }

        public void UpdateCountry(Country Country)
        {
            _context.Countries.Update(Country);
        }

        public Country GetCountry(Country Country)
        {
           return _context.Countries.FirstOrDefault(x=> x.Name == Country.Name);
        }
        public Country GetCountryByNume(string nume)
        {
           return _context.Countries.FirstOrDefault(x=> x.Name == nume);
        }
        public List<Country> GetAll()
        {
            return _context.Countries.ToList();
        }
    }
}
