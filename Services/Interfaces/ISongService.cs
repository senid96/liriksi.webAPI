using liriksi.Model;
using liriksi.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services
{
    public interface ISongService
    {
        List<SongGetRequest> Get(SongSearchRequest request);
        SongGetRequest GetById(int id);
        List<SongGetRequest> GetSongsByAlbum(int id);
        SongGetRequest Insert(SongInsertRequest song);
        SongGetRequest Update(int id, SongInsertRequest song);
        bool Delete(int id);
    }
}
