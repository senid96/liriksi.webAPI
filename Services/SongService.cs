using AutoMapper;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.WebAPI.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace liriksi.WebAPI.Services
{
    public class SongService : ISongService
    {
        //context dependency injection
        private readonly LiriksiContext _context;
        private readonly IMapper _mapper;
        public SongService(LiriksiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public List<SongGetRequest> Get(SongSearchRequest request)
        {
            var query = _context.Song.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }
            if (!string.IsNullOrWhiteSpace(request?.Text))
            {
                query = query.Where(x => x.Text.Contains(request.Text));
            }
          
            var result = query.Include(x=>x.Album).Include(x=>x.Album.Performer).OrderBy(x => x.Title).Take(9).ToList();

            return _mapper.Map<List<SongGetRequest>>(result);
        }

        public SongGetRequest GetById(int id)
        {
            var entity = _context.Song.Where(x => x.Id.Equals(id)).Include(b=>b.Album.Genre).Include(b=>b.UsersSongRates).FirstOrDefault();
            return _mapper.Map<SongGetRequest>(entity);
        }

        public List<SongGetRequest> GetSongsByAlbum(int id)
        {
            List<Song> songs =  _context.Song.Where(x => x.AlbumId.Equals(id)).Include(b=>b.Album).Include(b=>b.Album.Genre).Include(b=>b.Album.Performer).ToList();
            return _mapper.Map<List<SongGetRequest>>(songs);
        }

        public SongGetRequest Insert(SongInsertRequest song)
        {        
            var entity = _mapper.Map<Song>(song);
            _context.Song.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<SongGetRequest>(_context.Song.Last());
        }
        
        public SongGetRequest Update(int id, SongInsertRequest obj)
        {
            var entity = _context.Song.Find(id);
            if (entity != null)
            {
                _context.Song.Attach(entity);
                _context.Song.Update(entity);

                entity.Title = obj.Title;
                entity.Text = obj.Text;
                entity.AlbumId = obj.AlbumId;

                _context.SaveChanges();
                return _mapper.Map<SongGetRequest>(entity);
            }
            return null;
        }

        public bool Delete(int id)
        {
            var entity = _context.Song.Find(id);
            if(entity != null)
            {
                _context.Song.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
