using liriksi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface IGenreService
    {
        List<Genre> Get(string genre);
        Genre Insert(Genre genre);
        Genre GetById(int id);
        Genre Update(int id, string name);
        bool Delete(int id);

    }
}
