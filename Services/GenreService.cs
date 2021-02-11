using liriksi.Model;
using liriksi.WebAPI.EF;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly LiriksiContext _context;
        public GenreService(LiriksiContext context)
        {
            _context = context;
        }

        public List<Genre> Get(string genre)
        {
            var query = _context.Genre.AsQueryable();
            if (genre==null)
                return query.ToList<Genre>();
            else
                return query.Where(x => x.Name.Contains(genre)).ToList<Genre>();
        }

        [HttpGet("{id}")]
        public Genre GetById(int id)
        {
            var entity = _context.Genre.Find(id);
            return entity;
        }
        public Genre Insert(Genre genre)
        {
            _context.Genre.Add(genre);
            _context.SaveChanges();

            return _context.Genre.Last();
        }

        public Genre Update(int id, string name)
        {
            var entity = _context.Genre.Find(id);
            _context.Attach(entity);
            _context.Update(entity);
            entity.Name = name;

            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _context.Genre.Find(id);
            if (entity != null)
            {
                _context.Genre.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
