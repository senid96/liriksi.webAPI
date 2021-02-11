using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface IAlbumService
    {
        List<Album> Get(AlbumSearchRequest title);
        Album GetById(int id);
        Album Insert(AlbumInsertRequest album);
        Album Update(int id, AlbumInsertRequest album);
        bool Delete(int id);
        List<Album> GetAlbumsByPerformerId(int id);


    }
}
