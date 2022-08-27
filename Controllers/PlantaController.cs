using apiWebFlutter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using apiWebFlutter.mappings;


namespace apiWebFlutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantaController : ControllerBase
    {
        private readonly db_a89c33_myplantsContext _db;

        public PlantaController(db_a89c33_myplantsContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _db.Planta.ToListAsync();

            return Ok(lista);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Plantum planta)
        {
            if (planta == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PlantFound = await _db.Planta.Where(x => x.Id == planta.Id).ToListAsync();
            if (PlantFound.Count() == 0)
            {
                return BadRequest(ModelState);
            }
            return Ok(PlantFound.FirstOrDefault());

        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarPlanta([FromBody] plantaDTO planta)
        {
            if (planta == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _db.AddAsync(planta.ToDatabase());
            await _db.SaveChangesAsync();
            return Ok();

        }
    }
}
