using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class StateRepository : IStateRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public StateRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaState(State State)
        {
            _context.States.Add(State);
        }

        public void AdaugaListaStates(List<State> listaState)
        {
            _context.States.AddRange(listaState);
        }

        public void UpdateState(State State)
        {
            _context.States.Update(State);
        }

        public State GetState(State State)
        {
           return _context.States.FirstOrDefault(x=> x.Name == State.Name);
        }
        public List<State> GetAll()
        {
            return _context.States.ToList();
        }

        public List<State> GetAllByCountryId(int countryId)
        {
            return _context.States.Where(x=> x.Country_Id == countryId).Include(x=> x.Country).ToList();
        }
        public State GetStateByCityId(int CityId)
        {
            return _context.States.Where(x=> x.Cities.Any(y=>y.Id==CityId)).FirstOrDefault();
        }
    }
}
