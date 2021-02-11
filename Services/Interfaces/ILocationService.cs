using liriksi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface ILocationService
    {
        List<City> GetCitiesByCountryId(int countryId);
        List<Country> GetCountries();
    }
}
