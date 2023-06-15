﻿using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class AtractieTuristicaRepository : IAtractieTuristicaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AtractieTuristicaRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaAtractieTuristica(AtractieTuristica City)
        {
            _context.AtractiiTuristice.Add(City);
        }

        public bool VerificaExistentaAtractieTuristica(string numeAtractie, int CityId)
        {
            return _context.AtractiiTuristice.Any(x=> x.Nume == numeAtractie && x.CityId == CityId);
        }

        public void AdaugaListaAtractiiTuristice(List<AtractieTuristica> listaAtractiiTuristice)
        {
            _context.AtractiiTuristice.AddRange(listaAtractiiTuristice);
        }

        public void UpdateAtractieTuristica(AtractieTuristica atractieTuristica)
        {
            _context.AtractiiTuristice.Update(atractieTuristica);
        }

        public AtractieTuristica GetAtractieTuristica(int atractieTuristicaId)
        {
           return _context.AtractiiTuristice.Where(x=> x.Id == atractieTuristicaId).Include(p=> p.Photos).FirstOrDefault();
        }
        public async Task<List<AtractieTuristica>> GetAll()
        {
            return await _context.AtractiiTuristice.Include(p=> p.Photos).ToListAsync();
        }
        public List<AtractieTuristica> GetAllByCityId(int CityId)
        {
            return _context.AtractiiTuristice.Where(x=> x.CityId == CityId).Include(p=> p.Photos).ToList();
        }
    }
}
