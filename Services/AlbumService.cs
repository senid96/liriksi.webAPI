using AutoMapper;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.album;
using liriksi.WebAPI.EF;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly LiriksiContext _context;
        private readonly IMapper _mapper;

        public AlbumService(LiriksiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Album> Get(AlbumSearchRequest album)
        {
            if(album.Name == null)
                return _context.Album.Include(b=>b.Genre).Include(x=>x.Performer).ToList();
            
            return _context.Album.Include(b => b.Genre).Include(x => x.Performer).Where(x => x.Name.Contains(album.Name)).ToList();
        }
        public Album GetById(int id)
        {
            var entity = _context.Album.Include(b=>b.Genre).Include(b=>b.Performer).SingleOrDefault(x=>x.Id == id);
            if (entity != null)
                return entity;
            else
                return null;
        }
        public Album Insert(AlbumInsertRequest album)
        {
            var entity = _mapper.Map<Album>(album);
            _context.Album.Add(entity);
            _context.SaveChanges();

            return _context.Album.Last(); ;
        }
        public Album Update(int id, AlbumInsertRequest album)
        {
            var entity = _context.Album.Find(id);
            _context.Album.Attach(entity);
            _context.Album.Update(entity);

            entity.Name = album.Name;
            entity.YearRelease = album.YearRelease;
            entity.GenreId = album.GenreId;
            entity.Image = album.Image;
            entity.PerformerId = album.PerformerId;

            _context.SaveChanges();

            return entity;
        }
        public bool Delete(int id)
        {
            var entity = _context.Album.Find(id);
            if (entity != null)
            {
                _context.Album.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Album> GetAlbumsByPerformerId(int id)
        {
            return _context.Album.Where(x => x.PerformerId == id).Include(b=>b.Genre).Include(b=>b.Performer).ToList();
        }
    }
}
