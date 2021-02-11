using liriksi.Model;
using liriksi.WebAPI.EF;
using liriksi.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly LiriksiContext _context;

        public LocationService(LiriksiContext context)
        {
            _context = context;
        }

        
        public List<City> GetCitiesByCountryId(int countryId)
        {
            return _context.City.Where(x => x.CountryId == countryId).ToList();
        }

        public List<Country> GetCountries()
        {
            return _context.Country.ToList();
        }
    }
}
