using AutoMapper;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.performer;
using liriksi.WebAPI.EF;
using liriksi.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services
{
    public class PerformerService : IPerformerService
    {
        private readonly LiriksiContext _context;
        private readonly IMapper _mapper;
        public PerformerService(LiriksiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Performer> Get(PerformerSearchRequest obj) //insertRequest.. same object used for search
        {
            var query = _context.Performer.AsQueryable();
            if (!string.IsNullOrWhiteSpace(obj.Name))
                query = query.Where(x => x.Name.Contains(obj.Name));
            if (!string.IsNullOrWhiteSpace(obj.Surname))
                query = query.Where(x => x.Surname.Contains(obj.Surname));
            if (!string.IsNullOrWhiteSpace(obj.ArtisticName))
                query = query.Where(x => x.ArtisticName.Contains(obj.ArtisticName));
            return query.ToList();
        }

        public Performer GetById(int id)
        {
            return _context.Performer.Find(id);
        }

        public Performer Insert(PerformerInsertRequest obj)
        {
            var entity = _mapper.Map<Performer>(obj);
            _context.Performer.Add(entity);
            _context.SaveChanges();

            return  _context.Performer.Last();
        }

        public Performer Update(int id, PerformerInsertRequest obj)
        {
            var entity = _context.Performer.Find(id);
            if (entity != null)
            {
                _context.Performer.Attach(entity);
                _context.Performer.Update(entity);

                entity.Name = obj.Name;
                entity.Surname = obj.Surname;
                entity.ArtisticName = obj.ArtisticName;

                _context.SaveChanges();

                return entity;
            }
            else
                return null;
        }

        public bool Delete(int id)
        {
            var entity = _context.Performer.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
