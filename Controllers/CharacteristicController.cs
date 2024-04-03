using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiscountElectronicsApi.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace DiscountElectronicsApi.Controllers
{
    [Route("api/characteristic")]
    [ApiController]
    public class CharacteristicController : ControllerBase
    {
        private readonly discount_electronicContext _context;

        public CharacteristicController(discount_electronicContext context)
        {
            _context = context;
        }

        // GET: api/AllGoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllGood>>> GetAllGoods()
        {
            if (_context.AllGoods == null)
            {
                return NotFound();
            }
            return await _context.AllGoods.ToListAsync();
        }

        // GET: api/AllGoods/5
        [HttpGet("good/{id}")]
        public async Task<ActionResult<IEnumerable<AllGood>>> GetAllGood(int id)
        {
          if (_context.AllGoods == null)
          {
              return NotFound();
          }
            var allGood = await _context.AllGoods.Where(c => c.IdGoods == id).ToListAsync();

            if (allGood == null)
            {
                return NotFound();
            }

            return allGood;
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<Dictionary<String, List<FullCharacteristic>>>> GetCategoryCharacteristic(int id)
        {
            if (_context.DistinctCharacteristics == null)
            {
                return NotFound();
            }
            var categoryCharacteristics = await _context.DistinctCharacteristics.Where(c => c.IdCategory == id).ToListAsync();

            if (categoryCharacteristics == null)
            {
                return NotFound();
            }
            Dictionary<String, List<FullCharacteristic>> mappedCharacteristics = new Dictionary<String, List<FullCharacteristic>>();
            foreach (var characteristic in categoryCharacteristics)
            {
                var variations = CollectionExtensions.GetValueOrDefault(mappedCharacteristics, characteristic.CharacteristicName, new List<FullCharacteristic>());
                variations.Add(characteristic);
                mappedCharacteristics[characteristic.CharacteristicName] = variations;
            }
            return mappedCharacteristics;
        }
    }
}
