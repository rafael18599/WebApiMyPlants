using apiWebFlutter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace apiWebFlutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantaController : ControllerBase
    {
        private readonly MyPlantsContext _db;

        public PlantaController(MyPlantsContext db)
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
    }
}
