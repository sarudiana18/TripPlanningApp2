using API.Entities;

namespace API.Interfaces
{
    public interface IStateRepository
    {
        void AdaugaState(State State);

        void AdaugaListaStates(List<State> listaStates);

        void UpdateState(State State);

        State GetState(State State);
        List<State> GetAll();
        List<State> GetAllByCountryId(int countryId);
        State GetStateByCityId(int CityId);
    }
}