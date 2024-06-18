using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UsuariosModel>>> GetAllUsers()
        {
            List<UsuariosModel> users = await _usuariosRepositorio.GetAll();
            var json = JsonSerializer.Serialize(users);
            return Ok(json);
        }

        [HttpGet("GetUserId/{id}")]
        public async Task<ActionResult<UsuariosModel>> GetUserId(int id)
        {
            UsuariosModel usuario = await _usuariosRepositorio.GetById(id);
            return Ok(usuario);
        }

        [HttpPost("Login")]
        public async Task<bool> Login(string email, string senha)
        {
            UsuariosModel usuario = await _usuariosRepositorio.Login(email, senha);
            if (usuario != null)
                return true;
            else
                return false;           
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UsuariosModel>> InsertUser([FromBody] UsuariosModel userModel)
        {
            UsuariosModel user = await _usuariosRepositorio.InsertUser(userModel);
            return Ok(user);
        }

        [HttpPut("UpdateUser/{id:int}")]
        public async Task<ActionResult<UsuariosModel>> UpdateUser(int id, [FromBody] UsuariosModel userModel)
        {
            userModel.UsuarioId = id;
            UsuariosModel user = await _usuariosRepositorio.UpdateUser(userModel, id);
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id:int}")]
        public async Task<ActionResult<UsuariosModel>> DeleteUser(int id)
        {
            bool deleted = await _usuariosRepositorio.DeleteUser(id);
            return Ok(deleted);
        }

    }
}
