namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        ICityRepository CityRepository {get;}
        IHotelRepository HotelRepository {get;}
        ICountryRepository CountryRepository {get;}
        IStateRepository StateRepository {get;}
        IAtractieTuristicaRepository AtractieTuristicaRepository {get;}
        IReviewRepository ReviewsRepository {get;}

        Task<bool> Complete();
        bool HasChanges();
    }
}