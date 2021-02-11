using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface IService<T, TSearch> //drugi parametar je zbog filtera
    {
        List<T> Get(TSearch search);
        T GetById(int id);
    }
}
