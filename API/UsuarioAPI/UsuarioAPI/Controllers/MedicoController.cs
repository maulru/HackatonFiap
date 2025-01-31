using Microsoft.AspNetCore.Mvc;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MedicoController : ControllerBase
    {
        /// <summary>
        /// Endpoint responsável por realizar o cadastro de um médico
        /// </summary>
        /// <returns></returns>
        [HttpPost("CadastrarMedico/")]
        public async Task<IActionResult> CadastrarMedico()
        {
            return Ok();
        }
    }
}
