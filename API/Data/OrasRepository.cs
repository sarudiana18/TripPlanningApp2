using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    internal class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CityRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaCity(City City)
        {
            _context.Cities.Add(City);
        }

        public void AdaugaListaCities(List<City> listaCities)
        {
            _context.Cities.AddRange(listaCities);
        }

        public void UpdateCity(City City)
        {
            _context.Cities.Update(City);
        }

        public City GetCity(City City)
        {
           return _context.Cities.FirstOrDefault(x=> x.Name == City.Name);
        }
        public List<City> GetAll()
        {
            return _context.Cities.ToList();
        }
        public List<City> GetAllByCountryId(int countryId)
        {
            return _context.Cities.Where(x=> x.Country_Id == countryId).ToList();
        }
        public List<City> GetAllByStateId(int stateId)
        {
            return _context.Cities.Where(x=> x.State_Id == stateId).ToList();
        }
    }
}
