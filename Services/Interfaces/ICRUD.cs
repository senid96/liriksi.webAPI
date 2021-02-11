using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface ICRUD<T, TSearch, TInsert, TUpdate> : IService<T, TSearch>
    {
        T Insert(TInsert request);

        T Update(int id, TUpdate request);
    }
}
