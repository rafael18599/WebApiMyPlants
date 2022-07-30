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
        private readonly db_a89c33_myplantsContext _db;

        public UsuarioController(db_a89c33_myplantsContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _db.Usuarios.ToListAsync();

            return Ok(lista);
        }

        [HttpGet("/{email}")]
        public async Task<IActionResult> GetUsuarioId(string email)
        {
            Usuario usuario = new Usuario();
            usuario.Email = email;
            var validar = await _db.Usuarios.Where(x => x.Email == usuario.Email).ToListAsync();
            if (validar.Count() > 0)
            {
                return Ok(validar.FirstOrDefault().Id);
            }          
            return BadRequest(ModelState);
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
            return Ok(userfound.FirstOrDefault());

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
