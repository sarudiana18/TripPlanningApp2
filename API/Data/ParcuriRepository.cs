using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class ParcRepository : IParcRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ParcRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaParc(Parc Parc)
        {
            _context.Parcuri.Add(Parc);
        }

        public void AdaugaListaParcuri(List<Parc> listaParcuri)
        {
            _context.Parcuri.AddRange(listaParcuri);
        }

        public void UpdateParc(Parc parc)
        {
            _context.Parcuri.Update(parc);
        }

        public Parc GetParc(int parcId)
        {
           return _context.Parcuri.Where(x=> x.Id == parcId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
        public Parc GetParcByPhotoId(int photoId)
        {
           return _context.Parcuri.Where(x=> x.Photos.Any(y=> y.Id == photoId)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
        public List<Parc> GetAll()
        {
            return _context.Parcuri.Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public List<Parc> GetAllByCityId(int cityId)
        {
            return _context.Parcuri.Where(x=> x.CityId == cityId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public bool VerificaExistentaParc(string numeParc, int CityId)
        {
            return _context.Parcuri.Any(x=> x.Nume == numeParc && x.CityId == CityId);
        }
    }
}
