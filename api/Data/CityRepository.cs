using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            return await _context.Cities.SingleOrDefaultAsync(
                c => c.Id == id);
        }
    }
}