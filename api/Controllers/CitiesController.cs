using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Authorize]
    public class CitiesController : BaseApiController
    {
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<City>> GetCities()
        {
            return await _cityRepository.GetCitiesAsync();
        }
    }
}