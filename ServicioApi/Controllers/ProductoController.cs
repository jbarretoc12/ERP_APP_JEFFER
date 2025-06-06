using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioApi.Custom;
using ServicioApi.Models;
using ServicioApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ServicioApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly BdpruebaContext _bdpruebaContext;
        public ProductoController(BdpruebaContext bdpruebaContext)
        {
            _bdpruebaContext = bdpruebaContext;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _bdpruebaContext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }
    }
}
