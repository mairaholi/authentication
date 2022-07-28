using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusCode.Models;

namespace StatusCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private SistemaContext _db = new SistemaContext();
        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _db.Usuario?.Add(usuario);
                _db.SaveChanges();  
            }
            catch
            {
                return BadRequest("Datos em formato incompatível.");
            }
            return Ok(usuario);
        }
        
        [HttpPost]
        [Route("Autenticar")]
        public ActionResult<dynamic> Autenticar(Credencial credencial)
        {
            var usuario = _db.Usuario.Where(Usuario => Usuario.Nome == credencial.Username && Usuario.Senha == credencial.Senha).FirstOrDefault();
            usuario.Senha = "";
            return Ok(usuario);
        }

        [HttpGet]
        [Route("Usuarios")]
        public ActionResult<List<Usuario>> RequererTodos()
        {
            // Consultei e armazenei todos os Usuário do banco de dados.
            var Usuarios = _db.Usuario?.ToList();

            // Retornei status 200 - Success, com todos os Usuários do banco de dados.
            return Ok(Usuarios);
        }

    }
}
