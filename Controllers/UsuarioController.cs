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
    public class UsuarioController : ControllerBase
    {
        private readonly MyPlantsContext _db;

        public UsuarioController(MyPlantsContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _db.Usuarios.ToListAsync();

            return Ok(lista);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if(usuario == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var validar = await _db.Usuarios.Where(x=>x.Email==usuario.Email).ToListAsync();
            if (validar.Count() > 0)
            {
                return BadRequest(ModelState);
            }
            await _db.AddAsync(usuario);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userfound = await _db.Usuarios.Where(x => x.Email == usuario.Email && x.Password == usuario.Password  ).ToListAsync();
            if(userfound.Count() == 0)
            {
                return BadRequest(ModelState);
            }
            return Ok(userfound);

        }
        [HttpPost("editar")]
        public async Task<IActionResult> Editar([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userfound = await _db.Usuarios.Where(x => x.Email == usuario.Email).ToListAsync();
            if (userfound.Count() == 0)
            {
                return BadRequest(ModelState);
            }
            userfound[0].Apellidos = usuario.Apellidos;
            userfound[0].Nombre = usuario.Nombre;
            userfound[0].Email = usuario.Email;
            _db.Usuarios.Update(userfound[0]);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
