using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public ICityRepository CityRepository => new CityRepository(_context, _mapper);
        public ICountryRepository CountryRepository => new CountryRepository(_context, _mapper);

        public IHotelRepository HotelRepository => new HotelRepository(_context, _mapper);
        public IStateRepository StateRepository => new StateRepository(_context, _mapper);

        public IAtractieTuristicaRepository AtractieTuristicaRepository => new AtractieTuristicaRepository(_context, _mapper);

        public IReviewRepository ReviewsRepository => new ReviewRepository(_context, _mapper);
        public IParcRepository ParcRepository => new ParcRepository(_context, _mapper);
        public IRestaurantRepository RestaurantRepository => new RestaurantRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}